﻿using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

//TODO 随着值更改进度条的颜色
//TODO 鼠标穿透？

namespace YearProgress
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //启动时钟
            _Timer1 = new System.Threading.Timer(UpdateDisplay, null, 0, _TimeInterval);
            UpdateDisplay(null);
        }

        private System.Threading.Timer _Timer1 = null;
        private int _TimeInterval = 1 * 1000;

        #region 窗口可拖动
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //可拖动，直接调用方法
            this.DragMove();
            
        }


        private void MainWindow1_Loaded(object sender, RoutedEventArgs e)
        {
            WindowLoaded = true;
        }
        #endregion
        /// <summary>
        /// 是否已经完成初始化
        /// </summary>
        bool WindowLoaded = false;

        private int floatPoint = 8;

        #region 更新进度条
        private void UpdateDisplay(object state)
        {
            int _y = DateTime.Now.Year;
            int _m = DateTime.Now.Month;
            int _d = DateTime.Now.Day;
            int _h = DateTime.Now.Hour;
            int _mi = DateTime.Now.Minute;

            progress_hour.Dispatcher.Invoke
                (
                    new Action
                    (
                        delegate
                        {
                            //更新小时进度条
                            int c = CalendarUtil.GetTimeValueByTimeUnit(DateTime.Now, CalendarUtil.TimeValueType.HOUR, CalendarUtil.TimeValueType.SECOND, CalendarUtil.TimeCalculationMode.CURRENT);
                            int t = CalendarUtil.GetTimeValueByTimeUnit(DateTime.Now, CalendarUtil.TimeValueType.HOUR, CalendarUtil.TimeValueType.SECOND, CalendarUtil.TimeCalculationMode.TOTAL)-1;
                            progress_hour.Value = (double)c / t * 100;
                            lbl_hour.Content = Math.Round((double)c / t * 100, 2) + "%";
                            //Debug.WriteLine("小时：" + c + "/" + t);
                        }
                    )
                );

            progress_month.Dispatcher.Invoke
                (
                    new Action
                    (
                        delegate
                        {
                            //更新月份进度条
                            int currentOfMonth = CalendarUtil.GetTimeValueByTimeUnit(DateTime.Now, CalendarUtil.TimeValueType.MONTH, CalendarUtil.TimeValueType.SECOND, CalendarUtil.TimeCalculationMode.CURRENT); 
                            int totalOfMonth = CalendarUtil.GetTimeValueByTimeUnit(DateTime.Now, CalendarUtil.TimeValueType.MONTH, CalendarUtil.TimeValueType.SECOND, CalendarUtil.TimeCalculationMode.TOTAL);
                            progress_month.Value = ((double)currentOfMonth / totalOfMonth) * 100;
                            lbl_month.Content = Math.Round((double)currentOfMonth / totalOfMonth * 100, 4) + "%";
                            //Debug.WriteLine("月：" + currentOfMonth + "/" + totalOfMonth);
                        }
                    )
                );

            progress_today.Dispatcher.Invoke(
                new Action
                (
                    delegate
                    {
                        //更新今天的进度条
                        double c = CalendarUtil.GetCurrentSecondOfDay();
                        double t = 86400.0 - 1;
                        double v = c / t * 100;
                        progress_today.Value = Math.Round(v,1);
                        lbl_today.Content = Math.Round(v, 3)  + "%";
                    }));

            progress_year.Dispatcher.Invoke(
                new Action
                (
                    delegate
                    {
                        //今年的进度
                        int year_MaxValue = CalendarUtil.GetTimeValueByTimeUnit(DateTime.Now, CalendarUtil.TimeValueType.YEAR, CalendarUtil.TimeValueType.SECOND, CalendarUtil.TimeCalculationMode.TOTAL) - 1;
                        int year_CurrentValue = CalendarUtil.GetTimeValueByTimeUnit(DateTime.Now, CalendarUtil.TimeValueType.YEAR, CalendarUtil.TimeValueType.SECOND, CalendarUtil.TimeCalculationMode.CURRENT);
                        progress_year.Value = ((double)year_CurrentValue) / year_MaxValue * 100;
                        lbl_year.Content = Math.Round((double)year_CurrentValue / year_MaxValue * 100, 5) + "%";
                        //Debug.WriteLine("年：" + year_CurrentValue + "/" + year_MaxValue);
                    }
                ));

            //分钟进度
            progress_minute.Dispatcher.Invoke(
                new Action
                (
                    delegate
                    {
                        int m = DateTime.Now.Second;
                        //因为从0开始，所以到59
                        progress_minute.Value = Math.Ceiling(m / 59.0 * 100);
                        lbl_minute.Content = Math.Round(m / 59.0 * 100, 0) + "%";
                    }));
        }
        #endregion


        /// <summary>
        /// 尺寸更改事件，调整渐变终点的位置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Element_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Point p = new Point(Width, 0);
            GradientBrush_Minute.EndPoint = p;
            GradientBrush_Day.EndPoint = p;
            GradientBrush_Hour.EndPoint = p;
            GradientBrush_Month.EndPoint = p;
            GradientBrush_Year.EndPoint = p;
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            var menu = (MenuItem)sender;
            string n = menu.Name;
            switch (n)
            {
                case "Exit":
                    this.Close();
                    break;
                case "MenuItem_ShowYear":
                    if (menu.IsChecked)
                    {
                        progress_year.Visibility = Visibility.Visible;
                        Panel_YearInfo.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        progress_year.Visibility = Visibility.Collapsed;
                        Panel_YearInfo.Visibility = Visibility.Collapsed;
                    }
                    break;
                case "MenuItem_ShowMonth":
                    if (menu.IsChecked)
                    {
                        progress_month.Visibility = Visibility.Visible;
                        Panel_MonthInfo.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        progress_month.Visibility = Visibility.Collapsed;
                        Panel_MonthInfo.Visibility = Visibility.Collapsed;
                    }
                    break;
                case "MenuItem_ShowDay":
                    if (menu.IsChecked)
                    {
                        progress_today.Visibility = Visibility.Visible;
                        Panel_DayInfo.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        progress_today.Visibility = Visibility.Collapsed;
                        Panel_DayInfo.Visibility = Visibility.Collapsed;
                    }
                    break;
                case "MenuItem_ShowHour":
                    if (menu.IsChecked)
                    {
                        progress_hour.Visibility = Visibility.Visible;
                        Panel_HourInfo.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        progress_hour.Visibility = Visibility.Collapsed;
                        Panel_HourInfo.Visibility = Visibility.Collapsed;
                    }
                    break;
                case "MenuItem_ShowMinute":
                    if (menu.IsChecked)
                    {
                        progress_minute.Visibility = Visibility.Visible;
                        Panel_MinuteInfo.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        progress_minute.Visibility = Visibility.Collapsed;
                        Panel_MinuteInfo.Visibility = Visibility.Collapsed;
                    }
                    break;
                default:
                    break;
            }
        }
    }
}

