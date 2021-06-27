using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Services
{   
    public class InfixToPostfix : IDataService
    {
        /// <summary>
        /// 計算機邏輯
        /// </summary>
        /// <param name="infix">中序表達式</param>
        /// <returns>後序表達式</returns>
        public Task<NumGroup.NumSingleResult> PostNumber(string infix)
        {
            return Task.Run(() =>
            {
                var postList = ToPostfix(infix); // 後序表達式
                var result = PostfixToNum(postList); // 運算結果
                NumGroup.NumSingleResult data = new NumGroup.NumSingleResult();
                var postfix = string.Join(",", postList.ToArray());
                var prefix = PostfixToPrefix(postList);

                data.Prefix = prefix;
                data.Formula = infix;
                data.Postfix = postfix;
                data.Result = result;
                return data;
            });
        }

        /// <summary>
        /// 優先權判定
        /// </summary>
        /// <param name="priority">優先權大小</param>
        /// <param name="c">待分析的數字</param>
        /// <returns>infix字串</returns>
        private static int Priority(int priority, char c)
        {
            // 定義每個運算子的權重
            if (c == '(')
            {
                return priority = -1;
            }
            else if (c == '+' || c == '-')
            {
                return priority = 5;
            }
            else if (c == '*' || c == '/')
            {
                return priority = 9;
            }
            else if (c == ')')
            {
                return priority = -100;
            }
            return 0;
        }

        /// <summary>
        /// 後序轉前序
        /// </summary>
        /// <param name="postfix">後序表達式</param>
        /// <returns>前序表達式</returns>
        private static string PostfixToPrefix(List<string> postfix)
        {
            Stack s = new Stack();
            for (int i = 0; i < postfix.Count; i++)
            {
                if (IsOperator(postfix[i]))
                {
                    string op1 = (string)s.Peek();
                    s.Pop();
                    string op2 = (string)s.Peek();
                    s.Pop();

                    // concat the operands and operator
                    string temp = postfix[i] + op2 + op1;

                    // Push String temp back to stack
                    s.Push(temp);
                }
                else
                {
                    // Push the operand to the stack
                    s.Push(postfix[i] + string.Empty);
                }
            }
            string ans = string.Empty;
            while (s.Count > 0)
            {
                ans += s.Pop();
            }               
            return ans;
        }

        /// <summary>
        /// 判別運算子
        /// </summary>
        /// <param name="x">輸入的數</param>
        /// <returns>運算子回傳true</returns>
        private static Boolean IsOperator(string x)
        {
            switch (x)
            {
                case "+":
                case "-":
                case "/":
                case "*":
                    return true;
            }
            return false;
        }

        /// <summary>
        /// 後序轉運算結果
        /// </summary>
        /// <param name="postfix">後序表達式</param>
        /// <returns>結果數值</returns>
        private static string PostfixToNum(List<string> postfix)
        {
            string answer;
            Stack<string> stack = new Stack<string>();
            double num2, num1, ans;
            for (int j = 0; j < postfix.Count; j++)
            {
                string c = postfix[j]; // 可支援轉型
                if (c.Equals("*"))
                {
                    string n1 = (string)stack.Pop();
                    string n2 = (string)stack.Pop();
                    num2 = Convert.ToDouble(n2);
                    num1 = Convert.ToDouble(n1);
                    ans = num2 * num1;
                    stack.Push(ans.ToString());
                }
                else if (c.Equals("/"))
                {
                    string n1 = (string)stack.Pop();
                    string n2 = (string)stack.Pop();
                    num2 = Convert.ToDouble(n2);
                    num1 = Convert.ToDouble(n1);
                    ans = num2 / num1;
                    stack.Push(ans.ToString());
                }
                else if (c.Equals("+"))
                {
                    string n1 = (string)stack.Pop();
                    string n2 = (string)stack.Pop();
                    num2 = Convert.ToDouble(n2);
                    num1 = Convert.ToDouble(n1);
                    ans = num2 + num1;
                    stack.Push(ans.ToString());
                }
                else if (c.Equals("-"))
                {
                    string n1 = (string)stack.Pop();
                    string n2 = (string)stack.Pop();
                    num2 = Convert.ToDouble(n2);
                    num1 = Convert.ToDouble(n1);
                    ans = num2 - num1;
                    stack.Push(ans.ToString());
                }
                else
                {
                    stack.Push(postfix[j]);
                }
            }
            answer = (string)stack.Pop();
            return answer;
        }        

        /// <summary>
        /// step1 : '(' pusg至stack
        /// step2 : ')':一直pop並將pop出的值放入postfix 直到stack中碰到 '('為止
        /// step3 : 運算元 0~9 直接放進postfix
        /// p.s 運算子有權重之分
        /// 使用堆疊處理 運算子權重比較:( => -1 , +- => 5 , */ => 9  權重大的壓得住小的 => push進stack
        /// </summary>
        /// <param name="infix"></param>
        /// <returns>後序表達式集合</returns>
        private List<string> ToPostfix(string infix)
        {
            int priority = 0; // 權重

            List<string> postList = new List<string>(); // 後序表達示

            Stack<char> stack = new Stack<char>();

            int recordLen = infix.Length; // 紀錄中序長度

            string temp = string.Empty; // 臨時變數 => 為了區分數字>10 和 是否有小數點  一碰到運算子就把前面的數字合併 塞進postfix 

            for (int i = 0; i < infix.Length; i++)
            {
                char c = infix[i];
                if (c == '+' || c == '-' || c == '*' || c == '/' || c == '(' || c == ')' || c == ')')
                {
                    int prior = Priority(priority, c); // 賦予優先權

                    if (prior == -1)
                    {
                        if (temp != string.Empty)
                        {
                            postList.Add(temp);
                            temp = string.Empty;
                        }
                        stack.Push(c);
                    }
                    else if (prior == 5)
                    {
                        if (temp != string.Empty)
                        {
                            postList.Add(temp);
                            temp = string.Empty;
                        }

                        if (stack.Count == 0)
                        {
                            stack.Push(c);
                        }
                        else if (stack.Peek() == '(') 
                        {
                            // '+' '-' 權重> '('
                            stack.Push(c);
                        }
                        else if (stack.Peek() == '*' || stack.Peek() == '/')
                        {
                            postList.Add(stack.Pop().ToString());
                            i--; // 重新回到這個運算子在run一次
                            recordLen++; // 記數也要加回去
                        }
                        else if (stack.Peek() == '+' || stack.Peek() == '-')
                        {
                            postList.Add(stack.Pop().ToString());
                            i--;
                            recordLen++;
                        }
                    }
                    else if (prior == -100)
                    {
                        if (temp != string.Empty)
                        {
                            postList.Add(temp);
                            temp = string.Empty;
                        }

                        while (stack.Peek() != '(') 
                        {
                            // 直到stack裡遇到'('把上面的運算子都pop出來
                            postList.Add(stack.Pop().ToString());
                        }
                        stack.Pop(); // 遇到的'('也要移掉
                    }
                    else if (prior == 9) 
                    {
                        // 遇到'*' '/'運算子
                        if (stack.Count == 0)
                        {
                            if (temp != string.Empty)
                            {
                                postList.Add(temp);
                                temp = string.Empty;
                            }
                            stack.Push(c);
                        }
                        else if (stack.Peek().ToString() == "*" || stack.Peek().ToString() == "/")
                        {
                            if (temp != string.Empty)
                            {
                                postList.Add(temp);
                                temp = string.Empty;
                            }
                            postList.Add(stack.Pop().ToString());
                            stack.Push(c);
                        }
                        else
                        {
                            postList.Add(temp);
                            temp = string.Empty;
                            stack.Push(c);
                        }
                    }
                }
                else
                {
                    temp += c;
                    // postfix += c; 數值直接帶進postfix
                }

                recordLen--; // 每循環一次 記數-1 

                if (recordLen == 0)
                {
                    while (stack.Count != 0)
                    {
                        if (temp != string.Empty)
                        {
                            postList.Add(temp);
                            temp = string.Empty;
                        }
                        postList.Add(stack.Pop().ToString());
                    }
                }
            }
            return postList;
        }        
    }
}
