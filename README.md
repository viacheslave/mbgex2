# mbgex2

**mbgex2** is a tool that mass-exports ERC Megabank Utilities bills.

This is v2, it should work with the latest [erc.megabank.ua](erc.megabank.ua) portal redesign.

The tool:
- shows accounts' data for the last month
- exports accounts' data into JSON and CSV formats for arbitrary period.

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

## Command-Line Usage (Windows)

### Parameters

* `-m`, `--mode`: tool's mode. Empty for console output, `export` for export
* `-u`, `--username`: account username, required
* `-p`, `--password`: account password, required
* `-f`, `--from`: start date, in `yyyy-MM-dd` format, optional
* `-t`, `--to`: end date, in `yyyy-MM-dd` format, optional

## Examples

Show last month:
```
mbgex2.exe -u <username> -p <password>
```

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

## Docker Usage

In order to display latest month's info for all accounts:

Build
```
$ docker build . -t mbgex2
```

Run
```
$ docker run --rm -it mbgex2
```

which prompts to enter MegaBank username/password.

NOTES:
- failed login attempts are not clearly handled by APIs, so that output data is empty.
- export is performed into current folder
- the tool check first three accounts (usually users have a single one) - it's not clear how to get the accounts list
- `start-date` and `end-date` dates are inclusive

## Output

1. Raw data. JSON files of what APIs give back.
2. CSVs. Data grouped by account ID and utility type.