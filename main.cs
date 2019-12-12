using System;
					
public class Program
{
	public static void Main(string[] args)
	{
		//Начальная сумма денег
    int cash = 10000;
    
    //Экземпляр класса Random для генерации случайных чисел
		var rand = new Random();

    //Пока у нас не кончились деньги
		while(cash>0)
		{
      //Стартуем раунд
			Game(ref rand, ref cash);

      //Показываем статистику
			ShowStats(ref cash);
		}
    
    //Увы, деньги кончились
    Console.WriteLine("Бабло кончилось");

    //Это, чтобы консоль не закрывалась
    Console.ReadKey();
	}
	
	static void Game(ref Random rand, ref int cash)
	{       
    ///Выбор ставки
    int intPlayerInput = GetCell();

    ///Выбор денежной ставки
    int intGameCash = GetBet(cash);
		
    //Генерация случайного числа 0..36 (Крутим рулетку)
    int randomValue = rand.Next(37);
    //Определение цвета выпавшей ячейки
    string color = CheckColor(randomValue);
		Console.WriteLine("Выпало {0} {1}", randomValue, color);

    //Переменная rate хранит значение коэффициента выигрыша. По умолчанию -1 (проигрыш)
		int rate = -1;

    //Проверяем, выиграл ли игрок
		switch (intPlayerInput)
		{
			case 38:
				//Игрок ввёл красное
				if(color == "красное") rate=1;				
				break;
			case 39:
        //Игрок ввёл чёрное
				if(color == "черное") rate=1;
				break;
			default:
				//Игрок ввёл число
				if(intPlayerInput == randomValue) rate = 35;
				break;
		};

    //Считаем выигрыш
		intGameCash *= rate;
		Console.WriteLine("Выигрыш: {0} $", intGameCash);
    //Добавляем к сумме денег
		cash += intGameCash;
	}
	
	static void ShowStats(ref int cash)
	{
		Console.WriteLine("Денег осталось: {0} $", cash);
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
      Console.WriteLine("Выбери ставку: 0-36, red или black");
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
          case "red":
            //Если пользователь ввёл "red", то возвращаем 38
            intPlayerInput = 38;
            break;
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

  static int GetBet(int cash)
  {
    //Переменная хранит значение денежной ставки
    int intGameCash = 0;
    //Пока не введена корректная денежная ставка - цикл
		while(intGameCash <= 0 || intGameCash > cash)
		{
			Console.WriteLine("Сколько ставить? ({0}$)", cash);
			//Ввод от пользователя
      string gameCash = Console.ReadLine();
      //Попытка перевести введённое значение в integer
			Int32.TryParse(gameCash, out intGameCash);
		};
    return intGameCash;
  }
}