using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawLine
{
    class SLF
    {
        private int XiangSu = 0;//像素
        private Graphics g = null;//绘图
        private Brush red, blue, black;           //几种画刷

        public SLF()
        {
            red = new SolidBrush(Color.Red);
            blue = new SolidBrush(Color.Blue);
            black = new SolidBrush(Color.Black);
        }

        /// <summary>
        /// 边的数据结构
        /// </summary>
        public class EDGE
        {
            public double xi;//边的下端点x坐标，在活化链表（AET）中，表示扫描线与边的交点x坐标
            public double dx;//是个常量（直线斜率的倒数）（x + dx, y + 1）
            public int ymax;//边的上顶点的y值
            public static bool operator <(EDGE a, EDGE b)//重载排列关系
            {
                return (Math.Abs(a.xi - b.xi)<1 ? a.dx < b.dx : a.xi < b.xi);
            }
            public static bool operator >(EDGE a, EDGE b)//重载排列关系
            {
                return (Math.Abs(a.xi - b.xi) < 1 ? a.dx > b.dx : a.xi > b.xi);
            }
            public static bool operator ==(EDGE a, EDGE b)//重载等于号
            {
                return (Math.Abs(a.xi - b.xi)<1  && a.dx == b.dx && a.ymax == b.ymax);
            }
            public static bool operator !=(EDGE a, EDGE b)//重载不等于号
            {
                return (Math.Abs(a.xi - b.xi)>1 || a.dx != b.dx || a.ymax != b.ymax);
            }
        }

        /// <summary>
        /// 扫描线更新算法核心思想：
        /// 扫描线算法的核心就是围绕“活动边表（AET）”展开的
        /// 为了方便活性边表的建立与更新，我们为每一条扫描线建立一个“新边表（NET）”
        /// 存放该扫描线第一次出现的边。
        /// 当算法处理到某条扫描线时，就将这条扫描线的“新边表”中的所有边逐一插入到“活动边表”中。
        /// “新边表”通常在算法开始时建立，建立“新边表”的规则就是：
        ///    如果某条边的较低端点（y坐标较小的那个点）的y坐标与扫描线y相等，
        ///    则该边就是扫描线y的新边，应该加入扫描线y的“新边表”。
        /// </summary>
        
       
        /// <summary>
        /// 扫面线多边形扫描算法
        /// </summary>
        /// <param name="Q">多边形点链表</param>
        public void ScanLinePolygonFill(List<Point> Q, Graphics g, int XiangSu)
        {
            this.XiangSu = XiangSu;
            this.g = g;

            List<EDGE>[] NET = new List<EDGE>[500];//定义新边表
            for (int i = 0; i < 500; i++) NET[i] = new List<EDGE>();//实例化

            int ymax=0, ymin=0;//多边形y的最大值和最小值

            GetPolygonMinMax(Q, out ymax, out ymin);//计算更新ymax和ymin(ok)
            InitScanLineNewEdgeTable(NET, Q, ymin, ymax);//初始化新边表
            HorizonEdgeFill(Q); //水平边直接画线填充
            ProcessScanLineFill(NET, ymin, ymax);
        }

        /// <summary>
        /// 获得更新多边形ymax和ymin
        /// </summary>
        /// <param name="Q"></param>
        /// <param name="ymax"></param>
        /// <param name="ymin"></param>
        private void GetPolygonMinMax(List<Point> Q, out int ymax, out int ymin)
        {
            ymax = -1;
            ymin = 1000;
            for (int i = 0; i < Q.Count; i++)
            {
                if (Q[i].Y > ymax) ymax = Q[i].Y;
                if (Q[i].Y < ymin) ymin = Q[i].Y;
            }
        }
        
        /// <summary>
        /// 初始化新边表
        /// 算法通过遍历所有的顶点获得边的信息，然后根据与此边有关的前后两个顶点的情况
        /// 确定此边的ymax是否需要-1修正。ps和pe分别是当前处理边的起点和终点，pss是起
        /// 点的前一个相邻点，pee是终点的后一个相邻点，pss和pee用于辅助判断ps和pe两个
        /// 点是否是左顶点或右顶点，然后根据判断结果对此边的ymax进行-1修正，算法实现非
        /// 常简单，注意与扫描线平行的边是不处理的，因为水平边直接在HorizonEdgeFill()
        /// 函数中填充了。
        /// </summary>
        private void InitScanLineNewEdgeTable(List<EDGE>[] NET, List<Point> Q, int ymin, int ymax)
        {
            List<int> temp = new List<int>();
            EDGE e;
            for (int i = 0; i < Q.Count; i++)
            {
                Point ps = Q[i];
                Point pe = Q[(i + 1) % Q.Count];
                Point pss = Q[(i - 1 + Q.Count) % Q.Count];
                Point pee = Q[(i + 2) % Q.Count];
                if (pe.Y != ps.Y)//不处理平行线
                {
                    e = new EDGE();
                    e.dx = (double)(pe.X - ps.X) / (double)(pe.Y - ps.Y) * XiangSu;
                    if (pe.Y > ps.Y)
                    {
                        e.xi = ps.X;
                        if (pee.Y >= pe.Y)
                            e.ymax = pe.Y - XiangSu;
                        else
                            e.ymax = pe.Y;
                        NET[ps.Y - ymin].Add(e);//加入对应的NET里
                        temp.Add(ps.Y - ymin);
                    }
                    else
                    {
                        e.xi = pe.X;
                        if (pss.Y >= ps.Y)
                            e.ymax = ps.Y - XiangSu;
                        else
                            e.ymax = ps.Y;
                        NET[pe.Y - ymin].Add(e);//加入对应的NET里
                        temp.Add(pe.Y - ymin);
                    }
                }
            }
            for (int i = 0; i < temp.Count; i++)
            {
                My_Sort(ref NET[temp[i]]);
            }
        }

        private void My_Sort(ref List<EDGE> list)
        {
            EDGE d = new EDGE();
            for (int i = 0; i < list.Count-1; i++)
            {
                for (int j = i + 1; j < list.Count; j++)//瞎！for (int j = i+1; i < list.Count; i++)
                {
                    if (list[j] < list[i])
                    {
                        d = list[j];
                        list[j] = list[i];
                        list[i] = d;
                    }
                }
            }
        }

        /// <summary>
        /// 水平边直接画线填充
        /// </summary>
        /// <param name="Q"></param>
        private void HorizonEdgeFill(List<Point> Q)
        {
            
        }

        /// <summary>
        /// 扫描线填充处理过程
        /// 开始对每条扫描线进行处理，对每条扫描线的处理有四个操作
        /// </summary>
        /// <param name="NET"></param>
        /// <param name="ymin"></param>
        /// <param name="ymax"></param>
        private void ProcessScanLineFill(List<EDGE>[] NET, int ymin, int ymax)
        {
            List<EDGE> AET=new List<EDGE>();//扫描线
            for (int y = ymin; y < ymax; y+=XiangSu)
            {
                #region 显示运算信息
                g.DrawLine(new Pen(red),new Point(10,y),new Point(20,y));
                g.DrawString(AET.Count.ToString(), new Font("微软雅黑", 6), blue, new Point(2, y));
                InsertNetListToAet(NET[y-ymin], ref AET);
                g.DrawString(y + " -> " + NET[y - ymin].Count + " -> " + AET.Count.ToString(), new Font("微软雅黑", 6), blue, new Point(25, y));
                for (int i = 0; i < AET.Count; i++)
                {
                    g.DrawString((((int)AET[i].xi) / XiangSu * XiangSu).ToString() + " ", new Font("微软雅黑", 6), blue, new Point(400 + i * 24, y));
                }
                #endregion
                FillAetScanLine(ref AET, y);
                RemoveNonActiveEdgeFromAet(ref AET, y);//删除非活动边
                UpdateAndResortAet(ref AET);//更新活动边表中每项的xi值，并根据xi重新排序
            }
        }

        /// <summary>
        /// 负责将扫描线对应的所有新边插入到aet中，插入操作到保证AET
        /// 还是有序表，插入排序的思想
        /// </summary>
        /// <param name="list"></param>
        /// <param name="AET"></param>
        private void InsertNetListToAet(List<EDGE> list, ref List<EDGE> AET)
        {
            if (list.Count == 0) return;
            if (AET.Count == 0)
            {
                AET = list; 
                return;
            }//刚开始这里写成if（）AET=list;return;一直出错！下次一定要规范！！！
            List<EDGE> temp = new List<EDGE>();
            int i = 0, j = 0;
            while (i < list.Count && j < AET.Count)
            {
                if (list[i] == AET[j])
                {
                    i++;
                    temp.Add(AET[j]);
                    j++;
                    continue;
                }
                if (list[i] < AET[j])
                {
                    temp.Add(list[i]);
                    i++;
                    continue;
                }
                if (list[i] > AET[j])
                {
                    temp.Add(AET[j]);
                    j++;
                    continue;
                }
            }
            while (i < list.Count)
            {
                temp.Add(list[i]);
                i++;
            }
            while (j < AET.Count)
            {
                temp.Add(AET[j]);
                j++;
            }
            AET = temp;
            //for (int i = 0; i < list.Count; i++)
            //{
            //    AET.Add(list[i]);
            //}
            //My_Sort(ref AET);
        }

        /// <summary>
        /// FillAetScanLine()函数执行具体的填充动作，
        /// 它将aet中的边交点成对取出组成填充区间，
        /// 然后根据“左闭右开”的原则对每个区间填充
        /// </summary>
        /// <param name="AET"></param>
        /// <param name="y"></param>
        private void FillAetScanLine(ref List<EDGE> AET, int y)
        {
            if (AET.Count < 2) return;
            y = y / XiangSu * XiangSu;
            for (int i = 0; i < AET.Count; i += 2)
            {
                int from = ((int)AET[i].xi + XiangSu) / XiangSu * XiangSu;
                int to = ((int)(AET[i + 1].xi + XiangSu / 2)) / XiangSu * XiangSu;
                while (from < to)
                {
                    Rectangle rect = new Rectangle(from - XiangSu / 2, y - XiangSu / 2, XiangSu, XiangSu);
                    g.FillEllipse(red, rect);
                    from += XiangSu;
                }
            }
        }

        /// <summary>
        /// 负责将对下一条扫描线来说已经不是“活动边”的边从aet中删除，
        /// 删除的条件就是当前扫描线y与边的ymax相等，如果有多条边满
        /// 足这个条件，则一并全部删除
        /// </summary>
        /// <param name="AET"></param>
        /// <param name="y"></param>
        private int line = 0;
        private void RemoveNonActiveEdgeFromAet(ref List<EDGE> AET, int y)
        {
            line = y;
            AET.RemoveAll(IsEdgeOutOfActive);
        }
        private bool IsEdgeOutOfActive(EDGE obj)
        {
            return line == obj.ymax;
        }

        /// <summary>
        /// 更新边表中每项的xi值，就是根据扫描线的连贯性用dx对其进行修正，
        /// 并且根据xi从小到大的原则对更新后的aet表重新排序
        /// </summary>
        /// <param name="AET"></param>
        private void UpdateAndResortAet(ref List<EDGE> AET)
        {
            AET.ForEach(UpdateAetEdgeInfo);//更新xi
            My_Sort(ref AET);
        }
        private void UpdateAetEdgeInfo(EDGE e)
        {
            e.xi += e.dx;
        }
    }
}
