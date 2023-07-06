using System;
using System.Windows.Input;

namespace _17_EFShop.ViewModels
{
	//в конструктор RelayCommand будем передавать лямбда-выражением действия команды,
	//второй параметр необязательный - делаем по умолчанию кнопку активной
	class RelayCommand : ICommand 
	{
		private Action<object> execute; 
		private Func<object, bool> canExecute;
		
		public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
		{
			this.execute = execute;
			this.canExecute = canExecute;
		}	

		public event EventHandler? CanExecuteChanged;	

		public bool CanExecute(object? parameter)
		{
			return canExecute == null || CanExecute(parameter); 
		}

		public void Execute(object? parameter)
		{
			execute(parameter);
		}
	}
}
