# Mega File Format

> ⚠️ **WARNING**: In an attempt to solidify and unite all Alamo-Engine related tools/libraries made by the Empire at War community, development has been moved to the consolidated [AlamoEngine-Tools/PetroglyphTools](https://github.com/AlamoEngine-Tools/PetroglyphTools) repository.

Implements the Petroglyph v1.0 meg archive file type in `C#`.

Special thanks to [@MikeLankamp](https://github.com/MikeLankamp) for decoding the files and hosting them on [Petrolution](http://modtools.petrolution.net/docs/MegFileFormat).

## Introduction

Mega Files (extension: .MEG) in Petroglyph's games are used to store the collection of game files. By packing these files into a single large file, the operating-system overhead of storing and opening each individual file is removed and it helps avoid fragmentation.

## Format Specification v1.0

Each Mega File begins with a header, followed by the Filename Table, the File Table and finally, the file data. All fields are in little-endian format.

### Header

| Offset | Name | Type | Description |
|:------:|:----:|:----:|:------------|
| `+0000h` | numFilenames | `uint32` | Number of filenames in the Filename Table |
| `+0004h` | numFiles | `uint32` | Number of files in the File Table |

### Filename Table Record

| Offset | Name | Type | Description |
|:------:|:----:|:----:|:------------|
| `+0000h` | length       | `uint16` | Length of the filename, in characters |
| `+0004h` | name         | `length` | The ASCII filename |

### File Table Record
| Offset | Name | Type | Description |
|:------:|:----:|:----:|:------------|
| `+0000h` | crc   | `uint32` | CRC-32 of the filename |
| `+0004h` | index | `uint32` | Index of this record in the table |
| `+0008h` | size  | `uint32` | Size of the file, in bytes |
| `+000Ch` | start | `uint32` | Start of the file, in bytes , from the start of the Mega File |
| `+0010h` | name  | `uint32` | Index in the Filename Table of the filename |
