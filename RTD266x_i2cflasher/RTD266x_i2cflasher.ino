/* Read, write, verify and erase the SPI flash chip connected to an RTD266X video chip 
   from an Arduino (32u4 tested, but ought to work with any)

   Essentially just a port of      https://github.com/ghent360/RTD-2660-Programmer 
   but handy if you want to have a 'standalone' programmer using something like
   a Feather Adalogger https://www.adafruit.com/products/2795

   Tested with RTD2662 but ought to work w/RTD2660 also (2668 uses a different protocol tho)

   Connect GND, SDA and SCL -> RTD programming pins (on some boards, they are on the VGA port)
      don't add any pullups, you'll use the 3.3V pu's on the RTD board
   Connect SCK, MOSI, MISO, CS -> MicroSD card
*/

#include "Wire.h"
#include "rtd266x_main.h"
#if defined(__AVR__)
   extern "C" { 
     #include "utility/twi.h"  // from Wire library, so we can do bus scanning
   }
#endif
#include <SoftwareSerial.h>

SoftwareSerial mySerial(A4, A5); // RX, TX

FlashDesc chip;
  
void setup(void) 
{
  while (!Serial);
  Serial.begin(250000);

  Wire.begin();
  Wire.setClock(400000);
}

void connection() {
#if defined(__AVR__)
  uint8_t data;
  if (twi_writeTo(0x4A, &data, 0, 1, 1)) {
    Serial.println(F("Couldn't find i2c device 0x4A"));
    return;
  }
  //Serial.println(F("Found 0x4A"));

  TWBR = ((F_CPU / TWI_FREQ) - 16) / 2;
#endif

  if (!WriteReg(0x6f, 0x80)) {  // Enter ISP mode
    Serial.println(F("Write to 0x6F failed"));
    return;
  }
  uint8_t b = ReadReg(0x6f);
  if (!(b & 0x80)) {
    Serial.println(F("Can't enable ISP mode"));
    return;
  }
  uint32_t jedec_id = SPICommonCommand(E_CC_READ, 0x9f, 3, 0, 0);
  Serial.print(F("JEDEC ID: 0x")); Serial.print(jedec_id, HEX);
  if (!FindChip(jedec_id, &chip)) {
    Serial.println(F("Unknown chip ID"));
    return;
  }
  
  Serial.print(F(" Manufacturer "));
  uint32_t id = GetManufacturerId(chip.jedec_id);
  switch (id) {
    case 0x20: Serial.print(F("ST"));         break;
    case 0xef: Serial.print(F("Winbond"));    break;
    case 0x1f: Serial.print(F("Atmel"));      break;
    case 0xc2: Serial.print(F("Macronix"));   break;
    case 0xbf: Serial.print(F("Microchip"));  break;
    case 0x5e: Serial.print(F("Zbit"));       break;
    default:   Serial.print(F("Unknown"));    break;
  }

  Serial.print(F(" Chip: "));      Serial.print(chip.device_name);
  Serial.print(F(" Size (KB): ")); Serial.print(chip.size_kb);
  Serial.println();
  SetupChipCommands(chip.jedec_id);
}

void erase() {
  Serial.print(F("Erasing..."));
  EraseFlash();
  Serial.println(F("done"));
}

void flash() {
  Serial.print(F("Connect..."));
  connection();
  ProgramFlash(chip.size_kb * 1024);
  WriteReg(0xEE, 0x02);
}

void save() {
  Serial.print(F("Connect..."));
  connection();
  SaveFlash(chip.size_kb * 1024);
  WriteReg(0xEE, 0x02);
}

char getch() {
  while (!Serial.available());
  return Serial.read();
}

#define hex2int(d) (d <= '9' ? d - '0' : d - 'A' + 10)
uint8_t hex2int8(uint8_t d1, uint8_t d2) {
  return hex2int(d2) << 4 | hex2int(d1) & 0xF;
}

uint8_t willCloseDebug = 0;
uint32_t st = 0;

void loop(void) 
{
  if (Serial.available()) {
    char cmd = Serial.read();
    
    uint32_t starttime = millis();
    
    switch (cmd) {
      case 'E':
        erase();
        break;
      case 'W':
        flash();
        break;
      case 'S':
        save();
        break;
      case 'I':
        Serial.println(F("RTD FLASH TOOL"));
        break;
      case 'C':
        connection();
        break;
      case 'A':
        connection();
        WriteReg(0x08, 0x82);
        while (ReadReg(0x08) & 0x80) {
          
        }
        Serial.println(ReadReg(0x09 + 3) >> 2);
        break;
      case 'D':
        Wire.end();
        mySerial.begin(19200);
        Serial.println("Open UART");
        break;
      case 'd':
        st = millis();
        mySerial.write(0x40 + 0xB0);
        willCloseDebug = 1;
      default:
        mySerial.write(cmd);
    }
  }

  if (willCloseDebug != 0 && st + 200 < millis()) {
    Serial.println("Close again");
    mySerial.write(0xF0);
    st = millis();
  }

  while (mySerial.available()) {
    uint8_t tmp = mySerial.read();
    Serial.write(tmp);
    if (willCloseDebug == 1 && tmp == 'O') willCloseDebug = 2;
    if (willCloseDebug == 2 && tmp == 'K') willCloseDebug = 3;
  }

  if (willCloseDebug == 3) {
    mySerial.end();
    Wire.begin();
    Serial.println("Close UART");
    willCloseDebug = 0;
  }
  
  //Serial.print(millis() - starttime); Serial.println(F(" ms"));
  /*
  if (cmd == 'R') {
     // open the file. note that only one file can be open at a time,
     // so you have to close this one before opening another.
     // Open up the file we're going to log to!
     dataFile = SD.open(SAVENAME, FILE_WRITE);
     if (! dataFile) {
       Serial.println(F("Error opening file"));
       return;
     }
    
    Serial.println(F("Dumping FLASH to disk"));
     if (! SaveFlash(&dataFile, chip.size_kb * 1024)) {
      Serial.println(F("**** FAILED ****"));
    }
    Serial.println(F("OK!"));
    dataFile.flush();
    dataFile.close();
    Serial.print(millis() - starttime); Serial.println(F(" ms"));
  }
  
  if (cmd == 'V') {
     dataFile = SD.open(READNAME, FILE_READ);
     if (! dataFile) {
       Serial.println(F("Error opening file"));
       return;
     }
    Serial.println(F("Verifying FLASH from disk"));
     if (! VerifyFlash(&dataFile, dataFile.size())) {
      Serial.println(F("**** FAILED ****"));
    }
    Serial.println(F("OK!"));
    dataFile.close();
    Serial.print(millis() - starttime); Serial.println(" ms");
  }  
  */
}
