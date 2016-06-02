using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace KellListBoxSelecter
{
    [DefaultEvent("SelectedIndexChanged")]
    public partial class ListBoxSelecter : UserControl
    {
        public ListBoxSelecter()
        {
            InitializeComponent();
        }

        List<object> allItems;
        [Browsable(true)]
        [Category("属性已更改")]
        [Description("选择项已改变")]
        public event EventHandler SelectedIndexChanged;

        [Browsable(true)]
        [Category("数据")]
        [Description("供选定项的集合")]
        public List<object> AllItems
        {
            get
            {
                return allItems;
            }
            set
            {
                allItems = value;
                listBox1.Items.Clear();
                if (allItems != null)
                    listBox1.Items.AddRange(allItems.ToArray());
            }
        }

        public void RemoveSourceItem(int index)
        {
            if (allItems != null && allItems.Count > 0 && index > -1 && index < allItems.Count)
                allItems.RemoveAt(index);
            if (listBox1.Items.Count > 0 && index > -1 && index < listBox1.Items.Count)
                listBox1.Items.RemoveAt(index);
        }

        public void UpdateSourceItem(int index, object newObj)
        {
            if (allItems != null && allItems.Count > 0 && index > -1 && index < allItems.Count)
                allItems[index] = newObj;
            if (listBox1.Items.Count > 0 && index > -1 && index < listBox1.Items.Count)
                listBox1.Items[index] = newObj;
        }

        [Browsable(false)]
        public int SelectedSourceIndex
        {
            get
            {
                return listBox1.SelectedIndex;
            }
        }

        [Browsable(false)]
        public List<object> SelectedItems
        {
            get
            {
                List<object> items = new List<object>();
                foreach (object o in listBox2.Items)
                {
                    items.Add(o);
                }
                return items;
            }
        }

        public void SelectSome(List<object> someItems)
        {
            listBox2.Items.Clear();
            listBox2.Items.AddRange(someItems.ToArray());
        }

        public void ReloadSourceData()
        {
            listBox1.Items.Clear();
            if (allItems != null)
                listBox1.Items.AddRange(allItems.ToArray());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox2.Items.Clear();
            listBox2.Items.AddRange(allItems.ToArray());
            listBox1.Items.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < listBox2.Items.Count; i++)
            {
                object item = listBox2.Items[i];
                if (!listBox1.Items.Contains(item))
                    listBox1.Items.Add(item);
            }
            listBox2.Items.Clear();
            /*
            listBox1.Items.Clear();
            listBox1.Items.AddRange(allItems);
            listBox2.Items.Clear();*/
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItems != null && listBox1.SelectedItems.Count > 0)
            {
                List<object> items = new List<object>();
                for (int i = 0; i < listBox1.SelectedItems.Count; i++)
                {
                    object item = listBox1.SelectedItems[i];
                    if (!listBox2.Items.Contains(item))
                    {
                        listBox2.Items.Add(item);
                        items.Add(item);
                    }
                }
                foreach (object o in items)
                {
                    listBox1.Items.Remove(o);
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (listBox2.SelectedItems != null && listBox2.SelectedItems.Count > 0)
            {
                List<object> items = new List<object>();
                for (int i = 0; i < listBox2.SelectedItems.Count; i++)
                {
                    object item = listBox2.SelectedItems[i];
                    if (!listBox1.Items.Contains(item))
                    {
                        listBox1.Items.Add(item);
                    }
                    items.Add(item);
                }
                foreach (object o in items)
                {
                    listBox2.Items.Remove(o);
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            listBox1.Sorted = true;
            listBox2.Sorted = true;
        }

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                object item = listBox1.SelectedItem;
                if (!listBox2.Items.Contains(item))
                {
                    listBox2.Items.Add(item);
                    listBox1.Items.Remove(item);
                }
            }
        }

        private void listBox2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                object item = listBox2.SelectedItem;
                if (!listBox1.Items.Contains(item))
                {
                    listBox1.Items.Add(item);
                }
                listBox2.Items.Remove(item);
            }
        }

        private void ListBoxSelecter_Resize(object sender, EventArgs e)
        {
            listBox1.Width = listBox2.Width = (this.Width - panel1.Width) / 2;
        }

        private void panel1_Resize(object sender, EventArgs e)
        {
            int add = (panel1.Height - 198) / 2;
            button1.Top = 7 + add;
            button3.Top = 42 + add;
            button4.Top = 71 + add;
            button2.Top = 106 + add;
            button5.Top = 141 + add;
            button6.Top = 168 + add;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SelectedIndexChanged != null)
                SelectedIndexChanged(this, EventArgs.Empty);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ReloadSourceData();
        }
    }
}