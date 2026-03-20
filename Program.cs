using Microsoft.Extensions.DependencyInjection;
using PaymentProcessingSystem.DependencyInjection;
using PaymentProcessingSystem.Presentation.Menus;

var services = new ServiceCollection();

services.AddApplicationServices();

var serviceProvider = services.BuildServiceProvider();

var mainMenu = serviceProvider.GetRequiredService<MainMenu>();

mainMenu.Show();