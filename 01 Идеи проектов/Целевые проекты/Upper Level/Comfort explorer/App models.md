
## File interface

```csharp
public enum OpenFileFormatOptions
{
    Text,
    Binary,
    Image,
    Audio,
    Video
}

public class YourFile
{
    public byte[]? Metadata { get; set; }
    public byte[] Data { get; set; }

    #region Constructor

    public YourFile(string filePath)
    {
        // всевозможные проверки правильности формата пути,
        // существования пути,
        // использования зарезервированных названий (CON) и т.д.
    }

    #endregion
    
    #region File metadata properties

    public long Size { get; protected set; }
    
    public string Location { get; set; }
    public string NameWithoutExtension { get; set; }
    public string Extension { get; set; }
    public string Name => NameWithoutExtension + Extension;
    public string FullName { get; protected set; }
    public string? ActualFormat { get; protected set; }
    
    FileAttributes Attributes { get; set; }

    // There are Set methods as well, so i you need to specify setters
    public DateTime CreationDateTime => File.GetCreationTime(FullName);
    
    public DateTime LastAccessDateTime => File.GetLastWriteTime(FullName);
    
    public DateTime LastWriteDateTime => File.GetLastWriteTime(FullName);
    
    #endregion

    #region Instance operations

    public virtual void Open(params string[] args)
    {
        throw new NotImplementedException();
    }

    // use default app associated with chosen format
    public void OpenAs(OpenFileFormatOptions formatType)
    {
        throw new NotImplementedException();
    }

    public void OpenWith(string executablePath)
    {
        try  
        {
            Process.Start(new ProcessStartInfo  
            {
                FileName = FullName,
                // Используем оболочку Windows для открытия файла
                UseShellExecute = true
            });
        }
        catch (Exception ex)  
        {
            Logger.Log($"Ошибка при открытии файла: {ex.Message}");  
        }
    }
    
    public virtual void Copy(string destination, bool overwrite = false) => File.Copy(FullName, destination, overwrite);
    
    public virtual void Move(string destination) => File.Move(FullName, destination);
    
    public virtual void Delete() => File.Delete(FullName);

    // Refreshes the state of the file
    public void Refresh()
    {
        throw new NotImplementedException();
    }

    public virtual YourFileCompareInfo CompareWith(YourFile other)
    {
        throw new NotImplementedException();
    }

    // Returns position of the first found piece of data
    public long ByteIndexOf(byte[] piece)
    {
        throw new NotImplementedException();
    }

    public long ByteIndexOf(string piece)
    {
        throw new NotImplementedException();
    }

    public long CharIndexOf(string piece)
    {
        throw new NotImplementedException();
    }

    public (long, long) LineAndPositionOf(string piece)
    {
        throw new NotImplementedException();
    }

    { // Полная чушь. Можно просто использовать [Flags] enum ReplaceOptions
        public void ReplaceFirst(byte[] data)
        {
            throw new NotImplementedException();
        }
    
        public void ReplaceFirst(string data)
        {
            throw new NotImplementedException();
        }
    
        public void ReplaceLast(byte[] data)
        {
            throw new NotImplementedException();
        }
    
        public void ReplaceLast(string data)
        {
            throw new NotImplementedException();
        }
    
        public void ReplaceAll(byte[] data)
        {
            throw new NotImplementedException();
        }
    }

    public void ReplaceAll(string data)
    {
        throw new NotImplementedException();
    }

    public bool TryDefineActualFormat(out string? format)
    {
        throw new NotImplementedException();
    }

    public void CreateShortcut(string? location = null)
    {
        location ??= Location;
        File.CreateSymbolicLink(location, FullName);
    }

    public bool TryConvertTo(string format)
    {
        throw new NotImplementedException();
    }
    
    #endregion

}
```

#Идеи_проектов #Идеи_проектов/Comfort_explorer