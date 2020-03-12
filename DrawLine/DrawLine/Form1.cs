using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO.Ports;
using System.Text.RegularExpressions;
using System.Threading;

namespace DrawLine
{
    public partial class form : Form
    {
        /// <summary>
        /// show==0表示连线状态
        /// show==tempP.Count表示已经连线连完，定时器关闭
        /// show在上述两者之间表示正在连线，定时器打开
        /// </summary>
        private int show = 0;
        List<Point> tempP;                      //用来存放Bresenham画线法计算得出的路径中的点
        public List<Point> P;                   //用来存储鼠标选定的点
        public Point tempp;                     //点的中间变量，用来中间过程使用

        public bool[] Vram = new bool[600 * 480];//用来记录颜色的数组
        public Point[] pSearch_8 = 
        { new Point(-1, 0), new Point(-1, 1), new Point(0, 1), new Point(1, 1),
          new Point(1, 0), new Point(1, -1), new Point(0, -1), new Point(-1, -1) 
        };                                      //八联通填充八个方向偏移（原始偏移)
        public Point[] pSearch_4 = 
        { new Point(-1, 0), new Point(0, 1), new Point(1, 0), new Point(0, -1) 
        };                                      //四联通填充四个方向偏移
        //过程中经过计算过的偏移（刚开始没分清结果每次只能第一次运行正确，发现是每次都累乘XiangSu了）
        public Point[] direction_8 = new Point[8];
        public Point[] direction_4 = new Point[4];

        private Brush red,blue,black;           //几种画刷
        private Graphics g;                     //绘图
        private int XiangSu = 2;                //点的像素2x2,点击点是正常点的2倍,下拉框可以选择多种

        SLF slf;                                //扫描线处理类

        public form()
        {
            InitializeComponent();
            g = CreateGraphics();
            tempP = new List<Point>();
            P = new List<Point>();
            red = new SolidBrush(Color.Red);
            blue = new SolidBrush(Color.Blue);
            black = new SolidBrush(Color.Black);
            //像素下拉框，用于选择像素
            PortList.Items.Clear();
            PortList.Items.Add("2x2");
            PortList.Items.Add("4x4");
            PortList.Items.Add("6x6");
            PortList.Items.Add("8x8");
            PortList.SelectedIndex = 0;//选中第一个
            //初始化Vram
            for (int i = 0; i < 600 * 480; i++)
                Vram[i] = false;
            checkBox.Checked = true;
            //扫描线类
            slf = new SLF();
        }

        /// <summary>
        /// 鼠标点击事件回调函数
        /// 用来描点、标记、存储
        /// </summary>
        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            //show==0表示连线状态
            //show==tempP.Count表示已经连线连完，定时器关闭
            //show在上述两者之间表示正在连线，定时器打开
            if (show != 0) return;
            if(PortList.Enabled==true) PortList.Enabled = false;//当已经开始选择点的时候就不能修改像素点了
            tempp.X = e.X / XiangSu * XiangSu;//取像素的整数倍Very Important
            tempp.Y = e.Y / XiangSu * XiangSu;
            P.Add(tempp);
            Rectangle rect = new Rectangle(tempp.X - XiangSu, tempp.Y - XiangSu, XiangSu * 2, XiangSu * 2); //鼠标点击要大一点2倍像素点，好看
            g.FillEllipse(blue, rect);
            g.DrawString(P.Count.ToString() + " : (" + tempp.X + "," + tempp.Y + ")", new Font("微软雅黑", 8), black, tempp);
        }

        /// <summary>
        /// 新建菜单消息回调函数
        /// 用来清除已经保存的点以及刷屏
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 新建ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (P.Count != 0)//清空队列
            {
                P.Clear();
            }
            if (tempP.Count != 0)//清空队列
            {
                tempP.Clear();
            }
            show = 0;
            PortList.Enabled = true;//使能PortList能够选择像素点
            g.Clear(Color.White);//刷新屏幕
            for (int i = 0; i < 600 * 480; i++)//初始化Vram
                Vram[i] = false;
        }

        /// <summary>
        /// 退出菜单消息回调函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// 连线Bresenham算法画线菜单回调函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 连线ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (P.Count > 2)//用BresenHam画线法画出找出所有的待画点放在tempP队列中
            {
                int i;
                for (i = 1; i < P.Count; i++)
                {
                    Bresenhamline(P[i - 1], P[i]);
                }
                Bresenhamline(P[i-1], P[0]);
                timer1.Enabled = true;//启动定时器
            }
            else
            {
                MessageBox.Show(this,"至少3个点！");
            }
        }
        
        /// <summary>
        /// Bresenham画线算法，给出两个点a,b
        /// </summary>
        /// PS:这里XiangSu是光栅的最小距离，因为我是在高分辨率的窗口中模拟低分辨率效果，
        /// 所以用这个XiangSu来控制模拟的光栅最小单元格的边的大小，实际运用中XiangSu=1
        /// <param name="a"></param>
        /// <param name="b"></param>
        void Bresenhamline(Point a, Point b)
        {
            int x, y, dx, dy, s1, s2, p, temp, interchange, i;
            x = a.X;
            y = a.Y;
            dx = Math.Abs(b.X - a.X);
            dy = Math.Abs(b.Y - a.Y);
            if (b.X > a.X)
                s1 = XiangSu;
            else
                s1 = -XiangSu;
            if (b.Y > a.Y)
                s2 = XiangSu;
            else
                s2 = -XiangSu;
            if (dy > dx)
            {
                temp = dx;
                dx = dy;
                dy = temp;
                interchange = 1;
            }
            else
                interchange = 0;
            p = 2 * dy - dx;
            for (i = 1; i <= dx; i += XiangSu)
            {
                tempp.X = x;
                tempp.Y = y;
                tempP.Add(tempp);
                Vram[x + y * 600] = true;//把该片内存置为边界标记
                if (p >= 0)
                {
                    if (interchange == 0)
                        y = y + s2;
                    else
                        x = x + s1;
                    p = p - 2 * dx;
                }
                if (interchange == 0)
                    x = x + s1;
                else
                    y = y + s2;
                p = p + 2 * dy;
            }
        }

        /// <summary>
        /// 帮助
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 帮助ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(this, "浙工大-李涛\n\n    http://www.cnblogs.com/zjutlitao/");    
        }

        /// <summary>
        /// 定时器，用来动态呈现绘制过程
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (show < tempP.Count)
            {
                Rectangle rect = new Rectangle(tempP[show].X - XiangSu / 2, tempP[show].Y - XiangSu / 2, XiangSu, XiangSu);
                g.FillEllipse(black, rect); 
                show++;
            }
            else 
            {
                timer1.Enabled=false;
            }
        }

        /// <summary>
        /// 下拉框选择像素回调函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PortList_SelectedIndexChanged(object sender, EventArgs e)
        {
            XiangSu = 2 * PortList.SelectedIndex + 2;//根据下拉框选择绘制像素
        }

        /// <summary>
        /// 洪泛填充[注入填充]
        /// 将所有联通区域内某种指定颜色的点都替换成另一种颜色
        /// 边界填充：只要是边界内的点无论是什么颜色，都替换成指定的颜色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void floodFillAlgorithmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tempp.X = tempP[3].X + XiangSu;//选取第4个点内侧（随机猜测）
            tempp.Y = tempP[3].Y + XiangSu;
            checkBox.Enabled = false;//让绘制过程中不能改变选择
            do_check();//也要检查一遍，不然会出现错误
            FloodSeedFill(tempp);
            checkBox.Enabled = true;//恢复
        }
        /// <summary>
        /// 当选择2*2填充时会栈溢出
        /// </summary>
        /// <param name="a"></param>
        void FloodSeedFill(Point a)
        {
            if (a.X < XiangSu / 2 || a.X > 508 || a.Y < XiangSu / 2 || a.Y > 440) return;//边界情况
            if(Vram[a.X + a.Y*600]==false)
            {
                Rectangle rect = new Rectangle(a.X - XiangSu / 2, a.Y - XiangSu / 2, XiangSu, XiangSu);
                g.FillEllipse(red, rect);
                Vram[a.X + a.Y * 600] = true;//标记已经走过
                if (checkBox.Checked == true)
                    for (int i = 0; i < 4; i++)
                    {
                        tempp.X = a.X + direction_4[i].X;
                        tempp.Y = a.Y + direction_4[i].Y;
                        FloodSeedFill(tempp);
                    }
                else
                    for (int i = 0; i < 8; i++)
                    {
                        tempp.X = a.X + direction_8[i].X;
                        tempp.Y = a.Y + direction_8[i].Y;
                        FloodSeedFill(tempp);
                    } 
            }
        }

        /// <summary>
        /// CheckBox状态改变监听事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBox_CheckedChanged(object sender, EventArgs e)
        {
            do_check();
        }
        void do_check()
        {
            if (checkBox.Checked == true)//刚开始写成Enable了结果错了
                for (int i = 0; i < 4; i++)
                {
                    direction_4[i].X = pSearch_4[i].X * XiangSu;
                    direction_4[i].Y = pSearch_4[i].Y * XiangSu;
                }
            else
                for (int i = 0; i < 8; i++)
                {
                    direction_8[i].X = pSearch_8[i].X * XiangSu;
                    direction_8[i].Y = pSearch_8[i].Y * XiangSu;
                }
        }

        /// <summary>
        /// 扫描线填充算法填充触发事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void scanLineFillingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //P.Clear();
            //XiangSu = 1;
            //Point a;
            //a = new Point(3, 4);
            //P.Add(a);
            //a = new Point(2, 10);
            //P.Add(a);
            //a = new Point(5, 7);
            //P.Add(a);
            //a = new Point(12, 11);
            //P.Add(a);
            //a = new Point(15, 5);
            //P.Add(a);
            //a = new Point(12, 2);
            //P.Add(a);
            //a = new Point(7, 2);
            //P.Add(a);
            slf.ScanLinePolygonFill(P,g,XiangSu);
        }
    }
}

