using System; 
using System.Collections.Generic; 
using System.ComponentModel;
using System.Data; 
using System.Drawing; 
using System.Linq; 
using System.Text;
using System.Threading; 
using System.Windows.Forms; 
 
namespace WindowsFormsApplication1 {     
    public partial class Form1 : Form     
    {       
        private List<Circle> Snake = new List<Circle>();      
        private Circle food = new Circle();       
        private int defaultInterval = 1000; 
 
        public Form1()         
        {            
            InitializeComponent();            
            StartGame();        
        } 
 
        private void StartGame()        
        {             
            new Settings();             
            TimeInterval();            
            lblGameOver.Visible = false;           
            Snake.Clear();             
            GenerateSnake();            
            lblpo.Text = Settings.Score.ToString();             
            GenerateFood();        
        } 
 
        private void GenerateFood()         
        {            
            int maxXPosition = pbCanvas.Size.Width / Settings.Width;            
            int maxYPosition = pbCanvas.Size.Height / Settings.Height; 
 
            Random random = new Random();             
            food = new Circle();           
            food.X = random.Next(0, maxXPosition);            
            food.Y = random.Next(0, maxYPosition);         
        } 
 
        private void UpdateScreen(object sender, EventArgs e)        
        {            
            if (Settings.gameOver == true)           
            {                
                if (Input.keyPressed(Keys.Enter))              
                {                    
                    StartGame();              
                }           
            }           
            else            
            { 
                if (Input.keyPressed(Keys.Right) && Settings.direction != Direction.Left)                     
                    Settings.direction = Direction.Right;                 
                else if (Input.keyPressed(Keys.Left) && Settings.direction != Direction.Right)                   
                    Settings.direction = Direction.Left;                
                else if (Input.keyPressed(Keys.Up) && Settings.direction != Direction.Down)                  
                    Settings.direction = Direction.Up;                 
                else if (Input.keyPressed(Keys.Down) && Settings.direction != Direction.Up)                 
                    Settings.direction = Direction.Down; 
 
                MovePlayer();           
            } 
 
            pbCanvas.Invalidate();         
        } 
 
        private void pbCanvas_Paint(object sender, PaintEventArgs e)       
        {             
            Graphics canvas = e.Graphics; 

            if (!Settings.gameOver)           
            {               
                Brush snakeColor; 
 
                for (int i = 0; i < Snake.Count; i++)               
                {                   
                    if (i == 0)                 
                        snakeColor = Brushes.Black;            
                    else                       
                        snakeColor = Brushes.Green; 
 
                    canvas.FillEllipse(snakeColor, new Rectangle(Snake[i].X * Settings.Width, Snake[i].Y * Settings.Height, Settings.Width, Settings.Height));                  
                    canvas.FillEllipse(Brushes.Red, new Rectangle(food.X * Settings.Width, food.Y * Settings.Height, Settings.Width, Settings.Height));                 
                }            
            }             
            else            
            {
                lblGameOver.Visible = true;             
            }       
        } 
 
        private void MovePlayer()        
        {            
            for (int i = Snake.Count - 1; i >= 0; i--)          
            {              
                if (i == 0)               
                {                  
                    switch (Settings.direction)                  
                    {                        
                        case Direction.Right:                      
                            Snake[i].X++; 
                            break;                      
                        case Direction.Left:                      
                            Snake[i].X--;                          
                            break;                         
                        case Direction.Up:                        
                            Snake[i].Y--;                      
                            break;                     
                        case Direction.Down:                   
                            Snake[i].Y++;                      
                            break;                    
                    } 
 
                    int maxXPosition = pbCanvas.Size.Width / Settings.Width;                    
                    int maxYPosition = pbCanvas.Size.Height / Settings.Height; 
 
                    if (Snake[i].X < 0 || Snake[i].Y < 0 || Snake[i].X >= maxXPosition || Snake[i].Y >= maxYPosition)                   
                    {                        
                        Die();                 
                    } 
 
                    for (int j = 1; j < Snake.Count; j++)                 
                    {                      
                        if (Snake[i].X == Snake[j].X && Snake[i].Y == Snake[j].Y)   
                        {                            
                            Die();                      
                        }                    
                    } 
 
                    if (Snake[0].X == food.X && Snake[0].Y == food.Y)                   
                    {                     
                        Eat();                
                    } 
 
                }               
                else               
                {                    
                    Snake[i].X = Snake[i - 1].X;              
                    Snake[i].Y = Snake[i - 1].Y;          
                }         
            }      
        } 
 
        private void Eat()      
        {          
            GenerateSnake(); 
 
            if (Snake.Count != 0)          
            {         
                if (Snake.Count % 3 == 0)            
                {            
                    Settings.Speed = Settings.Speed + 2;       
                }        
            } 
 
            lblScore.Text = Convert.ToString(Settings.Speed); 
 
 
            Settings.Score += Settings.Points;           
            lblpo.Text = Settings.Score.ToString();  
            TimeInterval();            
            GenerateFood();         
        } 
 
        private void Die()      
        {   
            Settings.gameOver = true;           
            MessageBox.Show("GAME OVER"); 
 
            DialogResult dr = MessageBox.Show("Game Over. Do you want to play more ?", "Want to play more?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question); 
 
            if (dr == DialogResult.Yes)        
            {             
                StartGame();       
            }          
            else if (dr == DialogResult.No)        
            {         
            }          
            else if (dr == DialogResult.Cancel)     
            {         
            }       
        } 
 
        private void Form1_KeyUp(object sender, KeyEventArgs e)       
        {             
            Input.changeState(e.KeyCode, false);    
        } 
 
        private void Form1_KeyDown(object sender, KeyEventArgs e)     
        {           
            Input.changeState(e.KeyCode, true);      
        } 
 
        private void GenerateSnake()      
        {            
            Circle head = new Circle();     
            head.X = 5;      
            head.Y = 5;       
            Snake.Add(head);     
        } 
 
        private void TimeInterval()   
        {            
            gameTimer.Interval = defaultInterval / Settings.Speed;       
            gameTimer.Tick += UpdateScreen;       
            gameTimer.Start();    
        } 

        private void Form1_Load(object sender, EventArgs e)        
        { 
 
        } 
 
        private void gameTimer_Tick(object sender, EventArgs e)       
        {

        }
 
    }
} 
 