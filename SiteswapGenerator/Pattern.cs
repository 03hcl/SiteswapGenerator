namespace SiteswapGenerator
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// パターンを格納します。
    /// </summary>
    public class Pattern
    {
        /// <summary>
        /// <see cref="Pattern"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        public Pattern(int period)
        {
            this.Period = period > 0 ? period : 0;
            this.Thrown = new List<long>(this.Period);
            this.Catched = new List<long>(this.Period);
        }

        /// <summary>
        /// ボールをキャッチする拍のリストを取得または設定します。
        /// </summary>
        public IList<long> Catched { get; }

        /// <summary>
        /// 拍ごとに投げられたボールの軌道のリストを取得または設定します。
        /// </summary>
        public IList<long> Thrown { get; }

        /// <summary>
        /// ボールをキャッチする拍のリストを取得または設定します。
        /// </summary>
        public IList<IList<long>> CatchedRotated { get; }

        /// <summary>
        /// 拍ごとに投げられたボールの軌道のリストを取得または設定します。
        /// </summary>
        public IList<IList<long>> ThrownRotated { get; }

        /// <summary>
        /// パターン長を取得します。
        /// </summary>
        public int Period { get; set; }

        public void
    }
}
