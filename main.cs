using System;
					
public class Program
{
  //Начальная сумма денег
  static int cash = 10000;

  //Экземпляр класса Random для генерации случайных чисел
	static Random rand = new Random();

  //Переменная rate хранит значение коэффициента выигрыша. По умолчанию -1 (проигрыш)
	static int rate = -1;

  //Переменная хранит ставку в $
  static int intGameCash = 0;

	public static void Main(string[] args)
	{        

    //Пока у нас не кончились деньги
		while(cash>0)
		{
      //Стартуем раунд
			Game();

      //Показываем статистику
			ShowStats();
		}
    
    //Увы, деньги кончились
    Console.WriteLine("Бабло кончилось");

    //Это, чтобы консоль не закрывалась
    Console.ReadKey();
	}
	
	static void Game()
	{       
    ///Выбор ставки
    int intPlayerInput = GetCell();

    ///Выбор денежной ставки
    GetBet();
		
    //Генерация случайного числа 0..36 (Крутим рулетку)
    int randomValue = rand.Next(37);
    //Определение цвета выпавшей ячейки
    string color = CheckColor(randomValue);
		Console.WriteLine($"Выпало {randomValue} {color}");

    //Проверяем, выиграл ли игрок
		switch (intPlayerInput)
		{
			case 38:
				//Игрок ввёл красное
				if(color == "красное") rate = Math.Abs(rate) * 2;
        else rate = -1;
				break;
			case 39:
        //Игрок ввёл чёрное
				if(color == "черное") rate = Math.Abs(rate) * 2;
        else rate = -1;
				break;
			default:
				//Игрок ввёл число
				if(intPlayerInput == randomValue) rate = Math.Abs(rate) * 35;
        else rate = -1;
				break;
		};

    //Считаем выигрыш
		intGameCash *= rate;
		
    if(rate > 0) 
    {
      //Если захочет преумножить выигрыш
      if(true)
      {
        Console.WriteLine($"Хотите преумножить выигрыш? (коэффициенты переумножаются при победе)");
      }
      else
      {
        
        Console.WriteLine($"Выигрыш: {intGameCash}$");

        //Добавляем к сумме денег
        cash += intGameCash;
        rate = -1;
        intGameCash = 0;
      }
    }
    else
    {
      //Добавляем к сумме денег
		  cash += intGameCash;
      rate = -1;
      intGameCash = 0;
    }
    
	}
	
	static void ShowStats()
	{
		Console.WriteLine($"Денег осталось: {cash} $");
	}
	
	static string CheckColor(int randomValue)
	{
		string color = "";
		switch(randomValue)
		{
			case 1:
			case 3:
			case 5:
			case 7:
			case 9:
			case 12:
			case 14:
			case 16:
			case 18:
			case 19:
			case 21:
			case 23:
			case 25:
			case 27:
			case 30:
			case 32:
			case 34:
			case 36:
				color = "красное";
				break;
			case 2:
			case 4:
			case 6:
			case 8:
			case 10:
			case 11:
			case 13:
			case 15:
			case 17:
			case 20:
			case 22:
			case 24:
			case 26:
			case 28:
			case 29:
			case 31:
			case 33:
			case 35:
				color = "черное";
				break;
			case 0:
			default:
				color = "зеленое";
				break;
		};
		return color;
	}

  static int GetCell()
  {
    //Пока не введена корректная ставка - цикл
    int intPlayerInput = -1;
    while(intPlayerInput == -1)
    {
      Console.WriteLine("Выбери ячейку: 0-36, red(r) или black(b)");
      //Ввод от пользователя (всегда string)
      string playerInput = Console.ReadLine(); 
      //Попытка перевести введенное значение в integer
      bool success = Int32.TryParse(playerInput, out intPlayerInput);
      //Если получилось перевести в число
      if (success)
      {
        //Проверяем, входит ли введенное число в диапазон чисел
        if(intPlayerInput < 0 || intPlayerInput > 36) intPlayerInput = -1;
      }
      //иначе (не получилось перевести в число)
      else 
      {            
        switch (playerInput.ToLower()) //переводим в нижний регистр
        {				              
          case "r":
          case "red":
            //Если пользователь ввёл "red", то возвращаем 38
            intPlayerInput = 38;
            break;
          case "b":
          case "black":
            //Если пользователь ввёл "black", то возвращаем 39
            intPlayerInput = 39;
            break;
          default:
            //Во всех остальных случаях просим ввести снова            
            intPlayerInput = -1;
            break;
        };
      };
    }
    return intPlayerInput;
  }

  static int GetBet()
  {
    //Пока не введена корректная денежная ставка - цикл
		while(intGameCash <= 0 || intGameCash > cash)
		{
			Console.WriteLine($"Сколько ставить? ({cash}$)");
			//Ввод от пользователя
      string gameCash = Console.ReadLine();
      //Попытка перевести введённое значение в integer
			Int32.TryParse(gameCash, out intGameCash);
		};
    return intGameCash;
  }
}