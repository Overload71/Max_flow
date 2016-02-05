using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MP
{
    public partial class Form1 : Form
    {
        Nodes n;
        int V;
        int[,] graph = new int[10, 10];

        public Form1()
        {
            InitializeComponent();

        }

        private void button1_Click(object sender, EventArgs e)
        {

            panel1.Refresh();
            n = new Nodes(panel1);
            // textBox1.Text = "5";
            V = Convert.ToInt32(textBox1.Text);
            //n.draw(Convert.ToInt32(textBox1.Text));
            for (int i = 0; i < Convert.ToInt32(textBox1.Text); i++)
            {
                comboBox1.Items.Add(i);
                comboBox2.Items.Add(i);
            }
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == comboBox2.Text || textBox2.Text == "")
            {
                MessageBox.Show("Error");
            }
            else
            {

                n.Draw_edge(Convert.ToInt32(comboBox1.Text), Convert.ToInt32(comboBox2.Text), textBox2.Text);
                graph[Convert.ToInt32(comboBox1.Text), Convert.ToInt32(comboBox2.Text)] = Convert.ToInt32(textBox2.Text);
                // graph[Convert.ToInt32(comboBox2.Text), Convert.ToInt32(comboBox1.Text)] = Convert.ToInt32(textBox2.Text);

            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            int S, T;
            S = Convert.ToInt32(textBox5.Text);
            T = Convert.ToInt32(textBox6.Text);
            textBox7.Text = Convert.ToString(fordFulkerson(graph, S, T));
        }
        bool bfs(int[,] rGraph, int s, int t, int[] parent)
        {
            // Create a visited array and mark all vertices as not visited
            bool[] visited = Enumerable.Repeat(false, V).ToArray();

            // Create a queue, enqueue source vertex and mark source vertex
            // as visited
            Queue<int> q = new Queue<int>();
            q.Enqueue(s);
            visited[s] = true;
            parent[s] = -1;

            // Standard BFS Loop
            while (q.Count() != 0)
            {

                int u = q.First();
                q.Dequeue();

                for (int v = 0; v < V; v++)
                {
                    if (visited[v] == false && rGraph[u, v] > 0)
                    {
                        q.Enqueue(v);
                        parent[v] = u;
                        visited[v] = true;
                    }
                }
            }

            // If we reached sink in BFS starting from source, then return
            // true, else false
            return (visited[t] == true);
        }
        void dfs(int[,] rGraph, int s, bool[] visited)
        {
            visited[s] = true;
            for (int i = 0; i < V; i++)
                if (rGraph[s, i] != 0 && !visited[i])
                    dfs(rGraph, i, visited);
        }
        int fordFulkerson(int[,] graph, int s, int t)
        {
            int u, v;

            int[,] rGraph = new int[V, V];

            for (u = 0; u < V; u++)
                for (v = 0; v < V; v++)
                    rGraph[u, v] = graph[u, v];

            int[] parent = new int[V];

            int max_flow = 0;


            while (bfs(rGraph, s, t, parent))
            {

                int path_flow = Int32.MaxValue;
                for (v = t; v != s; v = parent[v])
                {
                    u = parent[v];
                    path_flow = Math.Min(path_flow, rGraph[u, v]);
                }


                for (v = t; v != s; v = parent[v])
                {
                    u = parent[v];
                    rGraph[u, v] -= path_flow;
                    rGraph[v, u] += path_flow;
                }


                max_flow += path_flow;
            }
            bool[] visited = Enumerable.Repeat(false, V).ToArray();
            dfs(rGraph, s, visited);

            // Print all edges that are from a reachable vertex to
            // non-reachable vertex in the original graph
            for (int i = 0; i < V; i++)
                for (int j = 0; j < V; j++)
                    if (visited[i] && !visited[j] && graph[i, j] != 0)
                    {
                        String str = Convert.ToString(i);
                        str += " - ";
                        str+=Convert.ToString(j);
                        listBox1.Items.Add(str);
                    }
            return max_flow;
        }


        private void panel1_Click_1(object sender, EventArgs e)
        {
            Point p = panel1.PointToClient(Cursor.Position);
            n.set_nodes(p.X - 15, p.Y - 15);
            n.draw();
        }
    }
}
