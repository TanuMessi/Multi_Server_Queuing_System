using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Single_Server_Queuing_System
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        static int Q_LIMIT = 1000;
        int BUSY = 1;
        int IDLE = 0;

        int next_event_type;
        int num_custs_delayed;
        int num_delays_required;
        int num_events;
        int num_in_q;
        int server_status;

        double area_num_in_q;
        double area_server_status;
        double mean_interarrival;
        double mean_service;

        double mean_service_server2;

        double sim_time;
        double time_last_event;
        double total_of_delays;
        double[] time_arrival = new double[Q_LIMIT + 1];
        double[] time_next_event = new double[4];

        int num_custs_delayed_server2;
        int num_in_q_server2;
        int server_status_server2;

        double area_num_in_q_server2;
        double area_server_status_server2;
        double time_last_event_server2;
        double total_of_delayes_server2;

        static double[] time_arrival_server2 = new double[Q_LIMIT + 1];




        Random rand = new Random();

        public void only_Server1_Activity()
        {
            initialize();

            //run simulation if the condition full-fill
            while (num_custs_delayed < num_delays_required)
            {
                //next event determination
                timing();
                //update time-avg status
                // update_time_avg_status();
                // update_time_avg_status_2();


                // Invokation of appropriate event 
                switch (next_event_type)
                {
                    case 1:
                        this.arrive();
                        update_time_avg_status();
                        break;
                    case 2:
                        this.depart();
                        update_time_avg_status();
                        break;
                }
            }

            this.report();
            label6.Text = "Choice is <=0.70 "+"\nSo Not Enter Server 2";
            this.button_Calculate.Dispose();
        }

        /*public void both_server_Activity()
        {

        }*/


        private void button_Calculate_Click(object sender, EventArgs e)
        {
            num_events = 3;

            mean_interarrival = Convert.ToDouble(textBox1.Text);
            mean_service = Convert.ToDouble(textBox2.Text);
            mean_service_server2 = Convert.ToDouble(textBox4.Text);
            num_delays_required = Convert.ToInt32(textBox3.Text);
            label4.Text = "Mean Interarrival Time= " + textBox1.Text +
                "\n Mean Service Time= " + textBox2.Text +
                "\n Mean Service Time Server2= " + textBox4.Text +
                "\n Number Of Customers= " + textBox3.Text;

            double choice = rand.NextDouble();

            if(choice<=0.70)
            {
                only_Server1_Activity();
            }
            else
            {
                //simulation initialize
                initialize();

                //run simulation if the condition full-fill
                while (num_custs_delayed < num_delays_required)
                {
                    //next event determination
                    timing();
                    //update time-avg status
                    // update_time_avg_status();
                    // update_time_avg_status_2();


                    // Invokation of appropriate event 
                    switch (next_event_type)
                    {
                        case 1:
                            this.arrive();
                            update_time_avg_status();
                            break;
                        case 2:
                            this.depart();
                            update_time_avg_status();
                            this.arrive_2();
                            update_time_avg_status_2();
                            break;
                        case 3:
                            this.depart_2();
                            update_time_avg_status_2();
                            break;
                    }
                }


                while (num_in_q_server2 > 0)
                {
                    this.depart_2();
                    update_time_avg_status_2();
                    //MessageBox.Show("Sim Time: "+sim_time.ToString());
                }
                //this.depart_2();
                //this.depart_2();
                //report of simulation
                this.report();
                this.report_2();

                this.button_Calculate.Dispose();
            }
           

        }

        public void initialize()
        {
            //simulation clock initialize 
            sim_time = 0.0;
            //state variables initialize 
            server_status = IDLE;
            server_status_server2 = IDLE;

            num_in_q = 0;
            num_in_q_server2=0;
            
           
            time_last_event = 0.0;
            time_last_event_server2 = 0.0;
            //statistical counters initialize 
            num_custs_delayed = 0;
            num_custs_delayed_server2 = 0;

            total_of_delays = 0.0;
            total_of_delayes_server2 = 0.0;

            area_num_in_q = 0.0;
            area_num_in_q_server2 = 0.0;

            area_server_status = 0.0;
            area_server_status_server2=0.0;

            //event List initialize , If no customer then wrt( departure service) event should be eliminated

            time_next_event[1] = sim_time + Exponent_Funtion(mean_interarrival);
            time_next_event[2] = Math.Pow(10, 30);
            time_next_event[3] = Math.Pow(10, 30);
            // MessageBox.Show(time_next_event[2].ToString());
        }

        public void timing()
        {
            int i;
            double min_time_next_event = Math.Pow(10, 30);

            next_event_type = 0;

            //DETERMINE THE event type of next event happened 
            for (i = 1; i <= num_events; ++i)
            {
                if (time_next_event[i] < min_time_next_event)
                {
                    min_time_next_event = time_next_event[i];
                    next_event_type = i;
                }
            }
            //check if event list is empty 
            if (next_event_type == 0)
            {
                //here event list is zero
                MessageBox.Show("Event List is Empty" + sim_time.ToString());
                Application.Exit();
            }
            // when eventlist is not empty so simulation clock extend or advance 
            sim_time = min_time_next_event;

        }

        public void arrive()
        {
            double delay;
            // schedule the next arrival 

            time_next_event[1] = sim_time + Exponent_Funtion(mean_interarrival);
            //check if the server busy or idle 
            if (server_status == BUSY)
            {
                //server is busy , so increment number of customer in queue 
                ++num_in_q;
                //check if overflow happen 
                if (num_in_q > Q_LIMIT)
                {
                    MessageBox.Show("Overflow of the array time_arrival at :" + sim_time.ToString());
                    Application.Exit();

                }

                // store time arrival of customer
                time_arrival[num_in_q] = sim_time;
                
            }
            else
            {
                //server is IDLE , arriving customer have delay Zero 
                delay = 0.0;
                total_of_delays = total_of_delays + delay;
                //increment the number of customer delayed
                ++num_custs_delayed;
                //MessageBox.Show("Total Customer in Server1 :"+num_custs_delayed);
                server_status = BUSY;

                //schedule departure for sevice completion 

                time_next_event[2] = sim_time + Exponent_Funtion(mean_service);
            }

        }

        public void arrive_2()
        {
            double delay;
            // schedule the next arrival 

            //time_next_event[2] = time;
            // time_next_event[1] = sim_time + Exponent_Funtion(mean_interarrival);

            //check if the server busy or idle 
            if (server_status_server2 == BUSY)
            {
                //server is busy , so increment number of customer in queue 
                ++num_in_q_server2;
                //check if overflow happen 
                if (num_in_q_server2 > Q_LIMIT)
                {
                    MessageBox.Show("Overflow of the array time_arrival at :" + sim_time.ToString());
                    Application.Exit();

                }

                // store time arrival of customer
                time_arrival_server2[num_in_q_server2] = sim_time;
            }
            else
            {
                //server is IDLE , arriving customer have delay Zero 
                delay = 0.0;
                total_of_delayes_server2 = total_of_delayes_server2 + delay;
                //increment the number of customer delayed
                ++num_custs_delayed_server2;
                //MessageBox.Show("Total Customer in Server2 :" + num_custs_delayed_server2);
                server_status_server2 = BUSY;

                //schedule departure for sevice completion 
                //Edit Here With Mean Service 2
                time_next_event[3] = sim_time + Exponent_Funtion(mean_service_server2);
            }

           // MessageBox.Show("Arv time:" + sim_time);
        }


        public void depart()
        {
            int i;
            double delay;
            //check if queue is empty or not 
            if (num_in_q == 0)
            {
                //Queue is empty so the server should IDLE
                server_status = IDLE;
                time_next_event[2] = Math.Pow(10, 30);
            }
            else
            {
                //decrement number of customer bcz queue is non-Empty
                --num_in_q;
                //compute the delay of customer  who strat take service and update total delay
                delay = sim_time - time_arrival[1];
                total_of_delays = total_of_delays + delay;
                //MessageBox.Show(total_of_delays.ToString());
                //incrament the no of customer delay , and sdeparture schedule 
                ++num_custs_delayed;
                //MessageBox.Show("Total Customer in Server1 :" + num_custs_delayed);
                time_next_event[2] = sim_time + Exponent_Funtion(mean_service);

                // move each customer up 
                for (i = 1; i <= num_in_q; ++i)
                {
                    time_arrival[i] = time_arrival[i + 1];
                }

            }

           // MessageBox.Show("Depart=" + sim_time);
        }

        public void depart_2() 
        {
            int i;
            double delay;
            //check if queue is empty or not 
            if (num_in_q_server2 == 0)
            {
                //Queue is empty so the server should IDLE
                server_status_server2 = IDLE;
                time_next_event[3] = Math.Pow(10, 30);
            }
            else
            {
                //decrement number of customer bcz queue is non-Empty
                --num_in_q_server2;
                //compute the delay of customer  who strat take service and update total delay
                delay = sim_time - time_arrival_server2[1];
                total_of_delayes_server2 = total_of_delayes_server2 + delay;
                //MessageBox.Show(total_of_delays.ToString());
                //incrament the no of customer delay , and sdeparture schedule 
                ++num_custs_delayed_server2;
               // MessageBox.Show("Total Customer in Server2 :" + num_custs_delayed_server2);
                time_next_event[3] = sim_time + Exponent_Funtion(mean_service_server2);

                // move each customer up 
                for (i = 1; i <= num_in_q_server2; ++i)
                {
                    time_arrival_server2[i] = time_arrival_server2[i + 1];
                }
            }      
        }

        public void report()
        {
            //here the main performances 
            label5.Text = "Average Delay in Queue =" + (total_of_delays / num_custs_delayed).ToString()
                + "\n" +
                "Average Number in queue =" + (area_num_in_q / sim_time).ToString()
            + "\n" + "Server Utilization =" + (area_server_status / sim_time).ToString()
            + "\n" + "Simulation Ended = " + sim_time;
            // + "\n " + "Server Status =" + server_status
           // +"\n" + "Number Customer Delayed = " + num_custs_delayed;

        }

        public void report_2()
        {
            //here the main performances 
            label6.Text = "Average Delay in Queue =" + (total_of_delayes_server2 / num_custs_delayed_server2).ToString()
                + "\n" +
                "Average Number in queue =" + (area_num_in_q_server2 / sim_time).ToString()
            + "\n" + "Server Utilization =" + (area_server_status_server2 / sim_time).ToString()
            + "\n" + "Simulation Ended = " + sim_time;
            //+"\n " + "Server Status =" + server_status_server2
           // +"\n" + "Number Customer Delayed = " + (num_custs_delayed_server2);
           // +"\n" + "Number in Queue = " + num_in_q_server2;

        }

        public void update_time_avg_status()
        {
            double time_since_last_event;
            //compute of the time since last event occur and update or marking last event time 
            time_since_last_event = sim_time - time_last_event;
            time_last_event = sim_time;
            //update area under number in queue function
            area_num_in_q = area_num_in_q + (num_in_q * time_since_last_event);
            //update area under server busy indication
            area_server_status = area_server_status + (server_status * time_since_last_event);
        }

        public void update_time_avg_status_2()
        {
            double time_since_last_event;
            //compute of the time since last event occur and update or marking last event time 
            time_since_last_event = sim_time - time_last_event_server2;
            time_last_event_server2 = sim_time;
            //update area under number in queue function
            area_num_in_q_server2 = area_num_in_q_server2 + (num_in_q_server2 * time_since_last_event);
            //update area under server busy indication
            area_server_status_server2 = area_server_status_server2 + (server_status_server2 * time_since_last_event);
        }

        public double Exponent_Funtion(double needed_variable)
        {

            double value = -needed_variable * Math.Log(lcgrand());
            return value;
        }


        public double lcgrand()
        {

            double u = rand.NextDouble();
            return u;
        }

        private void button_Clear_Click(object sender, EventArgs e)
        {
            this.Close();         
        }


    }
}
