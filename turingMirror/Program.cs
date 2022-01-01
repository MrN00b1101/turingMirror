using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace turingMirror
{
    class rule
    {
        private char readFromTape1;
        private char actualState1;
        private char nextState1;
        private char writeToTape1;
        private int direction1;

        public char readFromTape { get => readFromTape1; set => readFromTape1 = value; }
        public char actualState { get => actualState1; set => actualState1 = value; }
        public char nextState { get => nextState1; set => nextState1 = value; }
        public char writeToTape { get => writeToTape1; set => writeToTape1 = value; }
        public int direction { get => direction1; set => direction1 = value; }// (-1 -> balra, 1->jobbra)
        public rule(char readFromTape, char actualState, char writeToTape, char nextState, int direction)
        {
            this.nextState = nextState;
            this.actualState = actualState;
            this.writeToTape = writeToTape;
            this.readFromTape = readFromTape;
            this.direction = direction;
        }
    }
    class turringMirrorClass
    {
        int i;
        //string input;
        char[] tape;
        List<rule> ruleSet;
        char state;//a;b;c;d;e
        
        public turringMirrorClass(string input, List<rule> ruleSet)
        {
            input = 'B' + input + 'B';
            this.i = 1;
            this.state = 'a';   
            this.tape = new char[input.Length];
            this.ruleSet = ruleSet;
            for(int j = 0; j<= tape.Length-1; j++)
            {
                tape.SetValue(input[j],j);
            }
            main();
        }
        public bool validate()
        {
            if (this.state != 'x') return true;
            return false;
        }
        public string fordit()
        {
            StringBuilder forditott = new StringBuilder();
            for (int j =1; j < tape.Length-1; j++) forditott.Append(tape[j]);
            return forditott.ToString();
        }
        private void main()
        {
            while (state != 'z' && state != 'x' )  delta();
        }
        private char read(){ return (char)tape.GetValue(i); }
        private void write(char c) { tape.SetValue(c, i); }
        
        private void delta()
        {
            bool find = false;
            foreach (var rule in ruleSet)
            {
                if(rule.readFromTape == read() && rule.actualState == this.state)
                {                   
                    this.write(rule.writeToTape);
                    this.state = rule.nextState;
                    this.i = this.i + rule.direction;
                    find = true;
                    break;
                }
               
            }
            if (!find) this.state = 'x';
            /*
             delta	  a	      b       c       d       e
                0	B b <	0 b <	0 c <	0 d >	1 d >
                1	B c <	1 b <	1 c <	0 e >	1 e >
                B	-----	B d >	B e >	0 a >	1 a >

             */
            /*
            switch (this.state)
            {
                case 'a':
                    switch ((char)this.tape.GetValue(i))
                    {
                        case '0':
                            this.write('B');
                            this.state = 'b';
                            this.i--;
                            break;
                        case '1':
                            this.write('B');
                            this.state = 'c';
                            this.i--;
                            break; ;
                        case 'B':
                            this.state = 'z';
                            break;
                    }
                    break;
                case 'b':
                    switch ((char)this.tape.GetValue(i))
                    {
                        case '0':
                            this.write('0');
                            this.state = 'b';
                            this.i--;
                            break;
                        case '1':
                            this.write('1');
                            this.state = 'b';
                            this.i--;
                            break;
                        case 'B':
                            this.write('B');
                            this.state = 'd';
                            this.i++;
                            break;
                    }
                    break;
                case 'c':
                    switch ((char)this.tape.GetValue(i))
                    {
                        case '0':
                            this.write('0');
                            this.state = 'c';
                            this.i--;
                            break;
                        case '1':
                            this.write('1');
                            this.state = 'c';
                            this.i--;
                            break;
                        case 'B':
                            this.write('B');
                            this.state = 'e';
                            this.i++;
                            break;
                    }
                    break;
                case 'd':
                    switch ((char)this.tape.GetValue(i))
                    {
                        case '0':
                            this.write('0');
                            this.state = 'd';
                            this.i++;
                            break;
                        case '1':
                            this.write('0');
                            this.state = 'e';
                            this.i++;
                            break; ;
                        case 'B':
                            this.write('0');
                            this.state = 'a';
                            this.i++;
                            break; ;
                    }
                    break;
                case 'e':
                    switch ((char)this.tape.GetValue(i))
                    {
                        case '0':
                            this.write('1');
                            this.state = 'd';
                            this.i++;
                            break; ;
                        case '1':
                            this.write('1');
                            this.state = 'e';
                            this.i++;
                            break; ;
                        case 'B':
                            this.write('1');
                            this.state = 'a';
                            this.i++;
                            break; ;
                    }
                    break;
            }
*/
        }
    }
    class Program

    {
        static void Main(string[] args)
        {
            List<rule> onlyBin = new List<rule>();
            onlyBin.Add(new rule('1', 'a', '1', 'a', 1));
            onlyBin.Add(new rule('0', 'a', '0', 'a', 1));
            onlyBin.Add(new rule('B', 'a', 'B', 'z', 0));
            onlyBin.Add(new rule('1', 'x', '1', 'x', 1));
            onlyBin.Add(new rule('0', 'x', '0', 'x', 1));
            onlyBin.Add(new rule('B', 'x', 'B', 'z', 0));
            Console.WriteLine("Kérem a bemenetet!");
            string input = Console.ReadLine();
            /*
             delta	  a	    x
                0	0 a > 0 x >
                1	1 a > 1 x >
                x   X x > X x >
                B	B z - B x >
             */
            turringMirrorClass validatedInput = new turringMirrorClass(input, onlyBin);
            while (!validatedInput.validate())
            {
                Console.WriteLine("Csak '0' és '1' karakter szerepelhet az inputban! Adj meg új bemenetet!");
                input = Console.ReadLine();
                validatedInput = new turringMirrorClass(input, onlyBin);
            }
            
            //  onlyBin.Add()

            /*
             delta	  a	      b       c       d       e
                0	B b <	0 b <	0 c <	0 d >	1 d >
                1	B c <	1 b <	1 c <	0 e >	1 e >
                B	B z -	B d >	B e >	0 a >	1 a >

             */
            List<rule> mirrorRules = new List<rule>();
            mirrorRules.Add(new rule('0', 'a', 'B', 'b', -1));
            mirrorRules.Add(new rule('0', 'b', '0', 'b', -1));
            mirrorRules.Add(new rule('0', 'c', '0', 'c', -1));
            mirrorRules.Add(new rule('0', 'd', '0', 'd', 1));
            mirrorRules.Add(new rule('0', 'e', '1', 'd', 1));

            mirrorRules.Add(new rule('1', 'a', 'B', 'c', -1));
            mirrorRules.Add(new rule('1', 'b', '1', 'b', -1));
            mirrorRules.Add(new rule('1', 'c', '1', 'c', -1));
            mirrorRules.Add(new rule('1', 'd', '0', 'e', 1));
            mirrorRules.Add(new rule('1', 'e', '1', 'e', 1));

            mirrorRules.Add(new rule('B', 'a', 'B', 'z', 0));
            mirrorRules.Add(new rule('B', 'b', 'B', 'd', 1));
            mirrorRules.Add(new rule('B', 'c', 'B', 'e', 1));
            mirrorRules.Add(new rule('B', 'd', '0', 'a', 1));
            mirrorRules.Add(new rule('B', 'e', '1', 'a', 1));
            turringMirrorClass turringMirrorClass = new turringMirrorClass(input, mirrorRules);
            turringMirrorClass.fordit();
            Console.WriteLine(turringMirrorClass.fordit());
        }
    }
}