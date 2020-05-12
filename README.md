# ISO 226
[![NuGet version (NokitaKaze.ISO226)](https://img.shields.io/nuget/v/NokitaKaze.ISO226.svg?style=flat)](https://www.nuget.org/packages/NokitaKaze.ISO226/)
<!--
[![Build status](https://ci.appveyor.com/api/projects/status/3fgpod9vvmgu45v8/branch/master?svg=true)](https://ci.appveyor.com/project/nokitakaze/dotnet-iso226/branch/master)
-->
[![Test status](https://img.shields.io/appveyor/tests/nokitakaze/dotnet-iso226.svg)](https://ci.appveyor.com/project/nokitakaze/dotnet-iso226/branch/master)

ISO 226 converter Db (decibels) <-> phon

## Using

You could read any media file, but you need to convert it to WAV.

```csharp
var phon = new NokitaKaze.ISO226.EqualLoudnessContour.ConvertDbToPhon(db, hz);
var db = new NokitaKaze.ISO226.EqualLoudnessContour.ConvertPhonToDb(phon, hz);
```

# References

- https://files.stroyinf.ru/Data2/1/4293820/4293820821.pdf ISO-226
- https://www.iso.org/standard/34222.html ISO-226

## Main table
![](https://nktkz.s3.eu-central-1.amazonaws.com/development/github/dotnet-iso226/meta/Lindos1.svg.png)

## Const tables
![](https://nktkz.s3.eu-central-1.amazonaws.com/development/github/dotnet-iso226/meta/table2.PNG)
![](https://nktkz.s3.eu-central-1.amazonaws.com/development/github/dotnet-iso226/meta/table3.PNG)
