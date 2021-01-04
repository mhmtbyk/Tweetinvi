using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tweetinvi;
using Tweetinvi.Parameters;
using Tweetinvi.Models;
using Tweetinvi.Controllers;
using Tweetinvi.Json;
using Tweetinvi.Streams.Model;
using Tweetinvi.Public;
using Tweetinvi.Models.DTO.QueryDTO;

namespace TwitterApi2
{
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();
            Auth.SetUserCredentials("eFlPiYpQXPdn23BQ3Iy2C6XLx", "uEdnUNGkFyqSMHRyhnaz89zYa7UsqJdhdrx5rkPvNKAnlLfACS", "494252514-y9BEgi7W4NfA9xszjmWWW3BWDDTiG898Tn1JZMmI", "76lp0c5Jida6btaXFcc5JNRTMIbhQlaCwOXfLVgSUcyy0");
        }     
        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            var homeTimelineParameter = new HomeTimelineParameters
            {
                MaximumNumberOfTweetsToRetrieve = 100,
            };
            var tweets = Timeline.GetHomeTimeline(homeTimelineParameter);
            foreach(var item in tweets)
            {
                listBox1.Items.Add(item.Text);
                listBox2.Items.Add(item.Id);
            } 
        }

        private void button2_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            var userTimelineParameters = new UserTimelineParameters();
            var tweets = Timeline.GetUserTimeline("Tolga_Cbn", userTimelineParameters);
  
            foreach (var item in tweets)
            {
                listBox1.Items.Add(item.Text);
                listBox2.Items.Add(item.Id);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            try
            {
                var text = textBox1.Text;
                var tweets = Tweet.PublishTweet(text);
                label1.Text = "İşlem Başarılı";
            }
            catch
            {
                label1.Text = "İşlem Başarısız";
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            label1.Text = String.Empty;
            listBox2.SelectedIndex = listBox1.SelectedIndex;
            
            try
            {
                var success = Tweet.DestroyTweet(long.Parse(listBox2.SelectedItem.ToString()));
                label1.Text="Tweet silindi";
                
            }
            catch
            {
                label1.Text = "İşlem Başarısız";
            }
            
        }
        

        private void button5_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            RateLimit.RateLimitTrackerMode = RateLimitTrackerMode.TrackAndAwait;
            var user = User.GetUserFromScreenName("Tolga_Cbn");
            
            var followerIds = user.GetFollowerIds(Int32.MaxValue);
            var follower = User.GetFollowers("Tolga_Cbn");

            foreach (var item in followerIds)
            {
                listBox2.Items.Add(item);
            }
            foreach (var item in follower)
            {
                listBox1.Items.Add(item);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            RateLimit.RateLimitTrackerMode = RateLimitTrackerMode.TrackAndAwait;
            var user = User.GetUserFromScreenName("Tolga_Cbn");

            var friendsIds = user.GetFriendIds(Int32.MaxValue);
            var friends = User.GetFriends("Tolga_Cbn");

            foreach (var item in friendsIds)
            {
                listBox2.Items.Add(item);
            }
            foreach (var item in friends)
            {
                listBox1.Items.Add(item);
            }
        }
    }
}
