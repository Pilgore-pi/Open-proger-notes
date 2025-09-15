namespace DBF_Viewer.Models;

internal struct D03DV_entity {
    public string Date { get; set; }                       // DATA
    public string EditDate { get; set; }                   // DAT_IZM
    public string LayoutNumber { get; set; }                    // N_MAK
    public string UBZ { get; set; }                             // UBZ
    public string CheckNumber { get; set; }                  // NSHCEK
    public string Detail { get; set; }                       // DETALE
    public string ManufacturingWorkshopNumber { get; set; }  // NC_IZG
    public string ReceivingWorkshopNumber { get; set; }      // NC_POL
    public string Area { get; set; }                         // UCH
    public string Amount { get; set; }                          // KOL
    public string Price { get; set; }                        // CENA
    public string RouteList { get; set; }                    // M_LIST
    public string EmployeeNumber { get; set; }               // TAB_N
    public string PR { get; set; }                              // PR
    public string Priority { get; set; }                        // PRIOR
    public string ProductionOrder { get; set; }              // NAR_ZAK
}
