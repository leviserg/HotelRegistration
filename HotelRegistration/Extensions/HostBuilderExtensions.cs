using HotelRegistration.Services;
using HotelRegistration.Stores;
using HotelRegistration.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace HotelRegistration.Extensions
{
    public static class HostBuilderExtensions
    {
        public static IHostBuilder AddViewModels(this IHostBuilder hostBuilder) {
            hostBuilder.ConfigureServices(services => {

                services.AddSingleton<ViewModelNavigationService<ReservationListViewModel>>();
                services.AddSingleton<ViewModelNavigationService<MakeReservationViewModel>>();

                services.AddTransient<ReservationListViewModel>((s) => CreateReservationListViewModel(s));
                services.AddSingleton<Func<ReservationListViewModel>>((s) => () =>
                    s.GetRequiredService<ReservationListViewModel>()
                );

                services.AddTransient<MakeReservationViewModel>();
                services.AddSingleton<Func<MakeReservationViewModel>>((s) => () =>
                    s.GetRequiredService<MakeReservationViewModel>()
                );

                services.AddSingleton<MainViewModel>();

            });

            return hostBuilder;
        }

        private static ReservationListViewModel CreateReservationListViewModel(IServiceProvider s)
        {
            return ReservationListViewModel.LoadViewModel(
                   s.GetRequiredService<ReservationCacheStore>(),
                   s.GetRequiredService<ViewModelNavigationService<MakeReservationViewModel>>()
                );
        }
    }
}
