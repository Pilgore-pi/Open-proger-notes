using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Windows;
using DBF_Viewer.Models;
using DbfDataReader;
using DotNetDBF;

namespace DBF_Viewer;

public partial class MainWindow : Window {
    
    public MainWindow() {
        InitializeComponent();
        DataContext = this;
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

        D03DV_entity stub = new D03DV_entity { 
            Amount = "10",
            Area = "15",
            CheckNumber = "#",
            Date = DateTime.Now.ToString(),
            EditDate = DateTime.Now.ToString(),
            Detail = "АГКР.ЛАЛАЛА.ТОПОЛЯ",
            EmployeeNumber= "505505",
            LayoutNumber="909",
            ManufacturingWorkshopNumber="024",
            Price="9000",
            Priority="2",
            ProductionOrder="0140Г024",
            UBZ="0140",
            RouteList="sjdfasdf"
        };

        LoadEntities_DbfTableReader_NugetPackage([stub, stub, stub, stub, stub, stub, stub, stub, stub, stub]);

        dgDataTable.ItemsSource = Entities;
    }

    private const string DBF_LOCATION = @"C:\Users\Prishutov\ME-Local\Obsidian vaults\Arbnotes\MyProjects\DBF viewer\DBF_Viewer\Resources\D03DV_nz.DBF";

    internal ObservableCollection<D03DV_entity> Entities { get; } = [];

    private void LoadEntitites() {

    }

    private void LoadEntities_DbfTableReader_NugetPackage(D03DV_entity[] newValues) {

        using DbfTable dbfTable = new (DBF_LOCATION, Encoding.GetEncoding(866));
        var header = dbfTable.Header;

        DbfRecord dbfRecord = new(dbfTable);

        D03DV_entity entity = new();
        Entities.Clear();
        
        while (dbfTable.Read(dbfRecord)) {
            int i = 0;
            entity.Date = dbfRecord.Values[i++].ToString();
            entity.EditDate = dbfRecord.Values[i++].ToString();
            entity.LayoutNumber = dbfRecord.Values[i++].ToString();
            entity.UBZ = dbfRecord.Values[i++].ToString();
            entity.CheckNumber = dbfRecord.Values[i++].ToString();

            entity.Detail = dbfRecord.Values[i++].ToString();
            entity.ManufacturingWorkshopNumber = dbfRecord.Values[i++].ToString();
            entity.ReceivingWorkshopNumber = dbfRecord.Values[i++].ToString();
            entity.Area = dbfRecord.Values[i++].ToString();
            entity.Amount = dbfRecord.Values[i++].ToString();

            entity.Price = dbfRecord.Values[i++].ToString();
            entity.RouteList = dbfRecord.Values[i++].ToString();
            entity.EmployeeNumber = dbfRecord.Values[i++].ToString();
            entity.PR = dbfRecord.Values[i++].ToString();
            entity.Priority = dbfRecord.Values[i++].ToString();

            entity.ProductionOrder = dbfRecord.Values[i].ToString();
            Entities.Add(entity);
        }

        return;

        Entities.Clear();
        foreach (var item in newValues) {
            Entities.Add(item);
        }
    }

    private void Button_Click(object sender, RoutedEventArgs e) {
        
        Entities[0] = new D03DV_entity {
            Amount = "10",
            Area = "15",
            CheckNumber = "#",
            Date = DateTime.Now.ToString(),
            EditDate = DateTime.Now.ToString(),
            Detail = "АГКР.ЛАЛАЛА.ТОПОЛЯ",
            EmployeeNumber = "505505",
            LayoutNumber = "909",
            ManufacturingWorkshopNumber = "024",
            Price = "9000",
            Priority = "2",
            ProductionOrder = "0140Г024",
            UBZ = "0140",
            RouteList = "sjdfasdf"
        };

        return;

        using Stream fos = File.Open(DBF_LOCATION, FileMode.Open, FileAccess.ReadWrite);
        using var writer = new DBFWriter();
        writer.CharEncoding = Encoding.GetEncoding(866);
        writer.LanguageDriver = 0x26; // Eq to CP866
        var field1 = new DBFField("DOCDATE", NativeDbType.Date);
        var field2 = new DBFField("DOCNUMBER", NativeDbType.Char, 50);

    }
}
