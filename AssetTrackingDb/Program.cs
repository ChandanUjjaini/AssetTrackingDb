


using AssetTrackingDb;
using System.Data;



Asset Assets = new Asset();

int Sel;

do
{
    MyDbContext Context = new MyDbContext();
    Console.WriteLine("*********************************");
    Console.WriteLine("* Select An Operation from Menu *");
    Console.WriteLine("*********************************");
    Console.WriteLine("* Show Asset\t  - \t 1\t*\n* Add Asset\t  -\t 2\t*\n* Sort\t\t  -\t 3\t*\n* Update Asset\t  -\t 4\t*\n* Remove Asset\t  -\t 5\t*\n* Load Sample Data-\t 6\t*\n* Exit\t\t  -\t 7\t*");
    Console.WriteLine("*********************************");

    Sel = AssetIO.Selchk("selection", 7);
    
    List<Asset> AssetD = Context.Assets.ToList();

    switch (Sel)
    {
        case 1:
            {
                // Code to add asset
                Console.Clear();
                AssetOp.Display(AssetD);
                break;
            }

        case 2:
            {
                // Code to add asset
                Console.Clear();
                AssetOp.Input();
                break;
            }
        case 3:
            {
                // Code to Display asset
                Console.Clear();
                AssetOp.SortList(AssetD);

                break;
            }
        case 4:
            {
                // Code to Update asset
                Console.Clear();
                AssetOp.UpdateAsset(AssetD);

                break;
            }
        case 5:
            {
                // Code to Remove asset
                Console.Clear();
                AssetOp.Delete(AssetD);

                break;
            }
        case 6:
            {
                // Code to Remove asset
                Console.Clear();
                AssetOp.LoadAsset();

                break;
            }
    }
} while (Sel != 7);
Console.ReadLine();

