App.xaml.cs -> OnStartUp
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

Main

            /*
            _reservations.Add(new ReservationViewModel(new Reservation(new Room(1, 1), "John Doe", DateTime.Now, DateTime.Now.AddDays(2))));
            _reservations.Add(new ReservationViewModel(new Reservation(new Room(1, 2), "Jane Smith", DateTime.Now, DateTime.Now.AddDays(3))));
            _reservations.Add(new ReservationViewModel(new Reservation(new Room(1, 3), "Nick Smart", DateTime.Now, DateTime.Now.AddDays(4))));
            _reservations.Add(new ReservationViewModel(new Reservation(new Room(1, 4), "Jack Fun", DateTime.Now, DateTime.Now.AddDays(5))));
            */