# mbgex2

**mbgex2** is a tool that mass-exports ERC Megabank Utilities bills.

This is v2, it should work with the latest [erc.megabank.ua](erc.megabank.ua) portal redesign.

## Prerequisites

- .NET 5 SDK
- Windows / Linux
- Optionally: GNU make, bash

## Build 

### Powershell

```
powershell ./build.ps1
```

### Make

```
$ make
```

## Usage (Windows)

Export since `start-date` to last month inclusive:
```
mbgex2.exe --mode export -u <username> -p <password> --from <start-date>
```

Export since `start-date` to `end-date` inclusive:
```
mbgex2.exe --mode export -u <username> -p <password> --from <start-date> --to <end-date>
```

Export last month:
```
mbgex2.exe --mode export -u <username> -p <password>
```

Show last month:
```
mbgex2.exe -u <username> -p <password>
```

NOTES:
- export is performed into `bin` folder
- the tool check first three accounts (usually users have a single one) - it's not clear how to get the accounts list
- dates are inclusive
- dates format preferably is `yyyy-MM-dd`, but parsing depends on your local settings
- dates should be in the following order: `start-date`, then `end-date`

## Output

1. Raw data. JSON files of what APIs give back.
2. CSVs. Data grouped by account ID and utility type.