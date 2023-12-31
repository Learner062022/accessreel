using AccessReelApp.database_structures;
using AccessReelApp.ViewModels;
using System.Diagnostics;

namespace AccessReelApp.Views;

public partial class InterviewsPage : ContentPage
{
    DatabaseControl databaseControl = new DatabaseControl();

    public InterviewsPage(InterviewsViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}

    async void TestDatabaseDefaultData()
    {
        List<TestStructure> testStructures = new List<TestStructure>();

        for (int i = 0; i < 20; i++)
        {
            TestStructure newTestStructure = new TestStructure();
            newTestStructure.name = $"structure {i}";
            newTestStructure.id = i;
            testStructures.Add(newTestStructure);
        }

        await databaseControl.SaveData(testStructures);
    }

    private void Saved_Clicked(object sender, EventArgs e)
    {
        TestDatabaseDefaultData();
        Debug.WriteLine("saved clicked");
    }

    private void Load_Clicked(object sender, EventArgs e)
    {
        TestLoadFromDataBase();
        Debug.WriteLine("load clicked");
    }

    async void TestLoadFromDataBase()
    {
        var loadedData = await databaseControl.LoadDataFromDatabaseAsync<TestStructure>();

        foreach (var item in loadedData)
        {
            Debug.WriteLine(item.name);
        }
    }
}