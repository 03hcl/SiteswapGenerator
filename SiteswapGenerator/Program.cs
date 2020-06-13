namespace SiteswapGenerator
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Numerics;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// プログラム。
    /// </summary>
    internal static class Program
    {
        /// <summary>
        /// メイン。
        /// </summary>
        /// <param name="args"> 引数。 </param>
        private static void Main(string[] args)
        {

        }

        /// <summary>
        /// サイトスワップを数え上げます。
        /// </summary>
        private static void CountSiteswapsMainSelector()
        {
            Console.WriteLine("コンソール画面に途中経過を出力する場合は Y, 出力しない場合はそれ以外を入力してください。");
            bool isOutputConsole = Console.ReadKey().Key == ConsoleKey.Y;

            Console.WriteLine();
            Console.WriteLine("指定したボール数まで全ての場合は A, 指定したボール数のみの場合は B を入力してください。");
            var key = Console.ReadKey().Key;

            switch (key)
            {
                case ConsoleKey.A:
                    CountSiteswapsAllMain(isOutputConsole);
                    break;
                case ConsoleKey.B:
                    CountSiteswapsAndOutputMain();
                    break;
                default:
                    Console.WriteLine();
                    Console.WriteLine("プログラムが実行されませんでした。");
                    break;
            }
        }

        /// <summary>
        /// 指定したボール、指定した周期までのサイトスワップを数え上げて、テキストファイルに出力します。
        /// </summary>
        /// <param name="isOutputConsole"> コンソール画面に途中経過を出力するかどうか。 </param>
        private static void CountSiteswapsAllMain(bool isOutputConsole)
        {
            int maxBalls;
            int maxPeriod;

            Console.WriteLine();
            Console.WriteLine("指定したボール、指定した周期までのサイトスワップの総数を求めます。");
            Console.WriteLine();

            do
            {
                Console.WriteLine("ボール数を入力してください。");
            }
            while (!int.TryParse(Console.ReadLine(), out maxBalls) || maxBalls <= 0);

            do
            {
                Console.WriteLine("周期を入力してください。");
            }
            while (!int.TryParse(Console.ReadLine(), out maxPeriod) || maxPeriod <= 0);

            using (var writer = new StreamWriter(maxBalls + "ball_" + maxPeriod + "period_siteswaps_chart.txt", false, Encoding.UTF8))
            using (var writerNotRepeated = new StreamWriter(maxBalls + "ball_" + maxPeriod + "period_siteswaps_chart_not_repeated.txt", false, Encoding.UTF8))
            {
                for (int balls = 1; balls <= maxBalls; balls++)
                {
                    var siteswaps = new List<BigInteger>();
                    var siteswapsNotRepeated = new List<BigInteger>();

                    if (isOutputConsole)
                    {
                        Console.WriteLine();
                        Console.WriteLine("【 " + balls + " ボール 】");
                    }

                    for (int period = 1; period <= maxPeriod; period++)
                    {
                        BigInteger siteswap = 0;

                        var pattern = CountPatterns(balls, period);

                        for (int i = 1; i < period; i++)
                        {
                            if (period % i == 0)
                            {
                                siteswap += siteswapsNotRepeated[i - 1];
                                pattern -= i * siteswapsNotRepeated[i - 1];
                            }
                        }

                        siteswapsNotRepeated.Add(pattern / period);
                        siteswaps.Add(siteswap + siteswapsNotRepeated.Last());

                        if (isOutputConsole)
                        {
                            Console.WriteLine("周期: {0} / 全 {1} 通り ({2} 通り)", period, siteswaps.Last(), siteswapsNotRepeated.Last());
                        }
                    }

                    writer.WriteLine(string.Join(",", siteswaps));
                    writerNotRepeated.WriteLine(string.Join(",", siteswapsNotRepeated));
                }
            }

            Console.WriteLine("いずれかのキーを押すと終了します。");
            Console.ReadKey(false);
        }

        /// <summary>
        /// サイトスワップを数え上げて結果をテキストファイルに出力します。
        /// </summary>
        private static void CountSiteswapsAndOutputMain()
        {
            int balls;
            int maxPeriod;

            Console.WriteLine();
            Console.WriteLine("n ボールの指定した周期までのサイトスワップの総数を求めます。");
            Console.WriteLine();

            do
            {
                Console.WriteLine("ボール数を入力してください。");
            }
            while (!int.TryParse(Console.ReadLine(), out balls) || balls <= 0);

            do
            {
                Console.WriteLine("周期を入力してください。");
            }
            while (!int.TryParse(Console.ReadLine(), out maxPeriod) || maxPeriod <= 0);

            Console.WriteLine();

            var siteswaps = new List<BigInteger>();
            var siteswapsNotRepeated = new List<BigInteger>();

            using (var writer = new StreamWriter(balls + "ball_" + maxPeriod + "period_siteswaps.txt", false, Encoding.UTF8))
            using (var writerNotRepeated = new StreamWriter(balls + "ball_" + maxPeriod + "period_siteswaps_not_repeated.txt", false, Encoding.UTF8))
            {
                for (int period = 1; period <= maxPeriod; period++)
                {
                    BigInteger siteswap = 0;

                    var pattern = CountPatterns(balls, period);

                    for (int i = 1; i < period; i++)
                    {
                        if (period % i == 0)
                        {
                            siteswap += siteswapsNotRepeated[i - 1];
                            pattern -= i * siteswapsNotRepeated[i - 1];
                        }
                    }

                    siteswapsNotRepeated.Add(pattern / period);
                    siteswaps.Add(siteswap + siteswapsNotRepeated.Last());

                    Console.WriteLine("周期: {0} / 全 {1} 通り ({2} 通り)", period, siteswaps.Last(), siteswapsNotRepeated.Last());

                    writer.WriteLine(siteswaps.Last());
                    writerNotRepeated.WriteLine(siteswapsNotRepeated.Last());
                }
            }

            Console.WriteLine("いずれかのキーを押すと終了します。");
            Console.ReadKey(false);
        }

        /// <summary>
        /// サイトスワップ数を数え上げます。
        /// </summary>
        private static void CountSiteswapsMain()
        {
            int balls;
            int maxPeriod;

            Console.WriteLine();
            Console.WriteLine("n ボールの指定した周期までのサイトスワップの総数を求めます。");
            Console.WriteLine();

            do
            {
                Console.WriteLine("ボール数を入力してください。");
            }
            while (!int.TryParse(Console.ReadLine(), out balls) || balls <= 0);

            do
            {
                Console.WriteLine("周期を入力してください。");
            }
            while (!int.TryParse(Console.ReadLine(), out maxPeriod) || maxPeriod <= 0);

            Console.WriteLine();

            var siteswaps = new List<BigInteger>();
            var siteswapsNotRepeated = new List<BigInteger>();

            for (int period = 1; period <= maxPeriod; period++)
            {
                BigInteger siteswap = 0;

                var pattern = CountPatterns(balls, period);

                for (int i = 1; i < period; i++)
                {
                    if (period % i == 0)
                    {
                        siteswap += siteswapsNotRepeated[i - 1];
                        pattern -= i * siteswapsNotRepeated[i - 1];
                    }
                }

                siteswapsNotRepeated.Add(pattern / period);
                siteswaps.Add(siteswap + siteswapsNotRepeated.Last());

                Console.WriteLine("周期: {0} / 全 {1} 通り ({2} 通り)", period, siteswaps.Last(), siteswapsNotRepeated.Last());
            }

            Console.WriteLine("いずれかのキーを押すと終了します。");
            Console.ReadKey(false);
        }

        /// <summary>
        /// パターン数を数え上げます。
        /// </summary>
        private static void CountPatternsMain()
        {
            do
            {
                int balls;
                int period;

                Console.WriteLine();
                Console.WriteLine("n ボールのサイトスワップの総数を求めます。");

                do
                {
                    Console.WriteLine("ボール数を入力してください。");
                }
                while (!int.TryParse(Console.ReadLine(), out balls) || balls <= 0);

                do
                {
                    Console.WriteLine("周期を入力してください。");
                }
                while (!int.TryParse(Console.ReadLine(), out period) || period <= 0);


                Console.WriteLine("全 {0} 通り", CountPatterns(balls, period, true));
                Console.WriteLine();

                Console.WriteLine("もう一度実行する場合は Enter キー, 終了する場合はそれ以外を入力してください。");
            }
            while (Console.ReadKey(false).Key == ConsoleKey.Enter);
        }

        /// <summary>
        /// パターン数を数え上げます。
        /// </summary>
        /// <param name="balls"> ボール数。 </param>
        /// <param name="period"> 周期。 </param>
        /// <param name="isConsole"> 途中経過をコンソールに出力するかどうか。 </param>
        /// <returns> パターン数。 </returns>
        private static BigInteger CountPatterns(int balls, int period, bool isConsole = false)
        {
            BigInteger result = 0;

            if (isConsole)
            {
                Console.WriteLine();
            }

            foreach (var block in EnumerateBlocks(balls, period))
            {
                int zeros = block.Count(b => b == 0);
                BigInteger pattern = BigInteger.Pow(Factorial(new BigInteger(period - zeros)), 2);

                for (int p = 1; p <= balls; p++)
                {
                    pattern /= Factorial(block.Count(b => b == p));
                }

                pattern *= E(period, zeros);

                result += pattern;

                if (isConsole)
                {
                    Console.WriteLine("({0})\t{1}通り", string.Join(", ", block), pattern);
                }
            }

            return result;
        }

        /// <summary>
        /// ブロックを列挙します。
        /// </summary>
        /// <param name="balls"> ボール数。 </param>
        /// <param name="period"> 周期。 </param>
        /// <returns> 列挙されたブロック。 </returns>
        private static IEnumerable<int[]> EnumerateBlocks(int balls, int period)
            => EnumerateBlocks(new int[period], period, 0, balls);

        /// <summary>
        /// ブロックを列挙します。
        /// </summary>
        /// <param name="block"> 列挙中のブロック。 </param>
        /// <param name="period"> 周期。 </param>
        /// <param name="index"> 指定されるブロックのindex。 </param>
        /// <param name="rest"> 残りの割り当て数。 </param>
        /// <returns> 列挙されたブロック。 </returns>
        private static IEnumerable<int[]> EnumerateBlocks(int[] block, int period, int index, int rest)
        {
            if (index + 1 == period)
            {
                block[index] = rest;
                yield return block;
            }
            else
            {
                for (int i = (rest + period - index - 1) / (period - index); i <= (index == 0 ? rest : Math.Min(rest, block[index - 1])); i++)
                {
                    block[index] = i;
                    foreach (var b in EnumerateBlocks(block, period, index + 1, rest - i))
                    {
                        yield return b;
                    }
                }
            }
        }

        /// <summary>
        /// E値を求めます。
        /// </summary>
        /// <param name="period"> 周期。 </param>
        /// <param name="zeros"> 0の数。 </param>
        /// <returns> E(x,y) </returns>
        private static BigInteger E(int period, int zeros)
        {
            int x = period - zeros + 1;
            BigInteger result = 0;
            BigInteger factorialX = Factorial(x);

            for (int i = 0; i <= x; i++)
            {
                result +=
                    (BigInteger.Pow(-1, x - i) * BigInteger.Pow(i, period + 1) * factorialX)
                    / (Factorial(i) * Factorial(x - i));
            }

            return result / factorialX;
        }

        /// <summary>
        /// 階乗を求めます。
        /// </summary>
        /// <param name="n"> n </param>
        /// <returns> n! </returns>
        private static BigInteger Factorial(BigInteger n)
        {
            if (n < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(n));
            }

            BigInteger result = 1;

            for (var i = n; i > 0; --i)
            {
                result *= i;
            }

            return result;
        }
    }
}
