﻿App.xaml.cs -> OnStartUp
 -> MainWindow to show + Bind DataContext to MainViewModel
	|-MainWindow.xaml - add binding to MainViewModel.CurrentViewModel (acts like container to grab ViewModels inside which are subclasses of ViewModelBase
	|-MainViewModel - keeps logic for handling  public prop CurrentViewModel
	
Models - have props and some derived logic like lambda expressions regarding business entities
Views - contains *.xaml 
ViewModels - used to bind properties within Views 

App.xaml.cs 
	-> MainWindow(.xaml).cs + DataContext of MainViewModel
		|-> MainWindow.xaml DataContext Binding of MainViewModel.CurrentViewModel + <views:{{ReservationListView or MakeReservationView}}
		|-> !!!! MainViewModel - set CurrentViewModel for instances of subclasses ViewModelBase (MakeReservationViewModel, ReservationListViewModel)
			-> MakeReservationView.xaml - Binding to ViewModels props + Input actions with "UpdateSourceTrigger=PropertyChanged"
				(DataContext will be passed by MainViewModel.CurrentViewModel - see prev. action)
				!! to be Binded the ViewModel should be subclass of ViewModelBase (with public event "PropertyChanged" and implemented method "OnPropertyChanged")
				Thus, additional mapping of regular Model with ViewModel required - see example of ReservationViewModel - this is interim model with mapping of "Reservation" fields (passed with constructor) to be seen from ReservationListView.xaml inside of "ListView.View" markup element + 
				+ Collection to be used within "ListView.View" should be Instance of ObservableCollection - which requires list of ViewModelBase instances - 
					- "Reservation"->"ReservationViewModel"->"ReservationListViewModel":ObservableCollection<ViewModelBase> "Reservations"->available for Binding in ReservationListView.xaml -> ListView.View
					
			Commands:
				-> ViewModel classes should contain properties of ICommand and pass delegated actions to the properties using constructor
{			
	OOP:
		abstract class	
			|-> abstract method - empty in abstract class and SHOULD be implemented in a subclass		
			|-> virtual method - contains some default logic and MAY be overrided in a subclass
}

Stores.NavigationStore - an example of Mediator object - it instantiates within App.xaml.cs as instance in constructor and passed to MainViewModel as a property to set CurrentViewModel.
-------------------
Binding dynamically ViewModels to MainWindow.xaml
1. Add namespace for Window xml element "xmlns:viewModels="clr-namespace:HotelRegistration.ViewModels"
2. Add "Grid.Resources" element to the "Grid" instead of hardcoded "<views:MakeReservationView DataContext="{...}">"
	<Grid.Resources>
		<DataTemplate DataType="{x:Type viewModels:MakeReservationViewModel}">
			<views:MakeReservationView/>
		</DataTemplate>
		<DataTemplate DataType="{x:Type viewModels:ReservationListViewModel}">
			<views:ReservationListView/>
		</DataTemplate>
	</Grid.Resources>
3. Add "ContentControl" to the "Grid" element (after "Grid.Resources")
	<ContentControl Content="{Binding CurrentViewModel}"/>
-------------------
Data Persistance w EntityFramework
0. Install SQL Express as an Docker Container Instance (see DockerNotes.txt)
1. Install NuGet package
	Microsoft.EntityFrameworkCore (check compatibility with .NET 8 version)
	Microsoft.EntityFrameworkCore.Tools 
		{
			Enables these commonly used commands:
			 - Add-Migration
			 - Bundle-Migration
			 - Drop-Database
			 - Get-DbContext
			 - Get-Migration
			 - Optimize-DbContext
			 - Remove-Migration
			 - Scaffold-DbContext
			 - Script-Migration
			 - Update-Database
		}
	Microsoft.EntityFrameworkCore.SqlServer
2. Create folders DbContext & DTOs (Data Transfer Object - used for transfer data btw app and db layer) + add Dto class + IDesignTimeDbContextFactory class 
	***
	public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<HotelReservationDbContext>
    {
        public HotelReservationDbContext CreateDbContext(string[] args)
        {
            string connectionString = "Server=127.0.0.1:1433;Database=HotelReservation;Uid=developer;Pwd=Xiaomi_MI3;";
            var options = new DbContextOptionsBuilder().UseSqlServer(connectionString).Options;
            return new HotelReservationDbContext(options);
        }
    }
	***
3. Add migration within Package manager console
	PM > add-migration Initial  (undo - Remove-Migration)
	PM > update-database (remove - if applied - Remove-Migration -Force)
	
4. Hosting WPF App:
	Install Microsoft.Extensions.Hosting NuGet package
	in App.xaml.xs add Host Builder
		
5. PUBLISH
	select project in VS 2022 - run command - "Open in Terminal"
	
	- dotnet publish -c Release { all app files within YourProject/bin/Release folder}
	
	- dotnet publish -c Release --self-contained {when no proper .net runtime version can be found on target PC}
		if failed - add <RuntimeIdentifier> key to your *.csproj PropertyGroup list - EG <RuntimeIdentifier>win-x64</RuntimeIdentifier> {find more on https://learn.microsoft.com/en-us/dotnet/core/rid-catalog}
		- all the executable files + runtime files within bin/Release/{targetPlatform - eg - net8.0-windows}/{target runtime identifier eg win-x64}/publish
		
	- dotnet publish -c Release --self-contained -p:PublishSingleFile=true
	