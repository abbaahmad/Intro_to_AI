using System;
using System.Collections.Generic;

namespace NeuralClassLibrary
{
    //Класс Персептрона
    public class Perceptron
    {
        //Данные:
        List <double> weights;  //Список весов
        Random rnd;             //Генератор случайных чисел
        double bais;            //Смещение
        double learning_rate;   //Скорость обучения

        //Свойства:
        public double DSigmoid     //Дельта т.е. Дифференциал от Сигмоида
        {
            get; private set;
        }
        public double FNet  //f(net)
        {
            get;
            set;
        }
        //public int NumOfW
        //{
        //    set;get;
        //}
        public List<double> Weights //Список весов
        {
            get { return weights; }
        }
        public double Learning_rate //Скорость обучения
        {
            set{learning_rate = value;}
            get{return learning_rate;}
        }

        //Функции:
        public Perceptron()     //Конструстор
        {
            rnd = new Random();     //Генератор случайных чисел
            weights = new List<double>();   //Список весов
            bais = rnd.NextDouble();        //Смещение
            learning_rate = 0.05;           //Скорость обучения
            // (0,9)%10);
        }
        //Иницилизация весов 
        private void LoadWeights(int num)
        {
            if (weights.Count != 0) return;
            for (int i = 0; i < num; i++)
                weights.Add(rnd.NextDouble());
        }
        //Вычисление Сигмоида
        private double Sigmoid(double sum)
        {
            return (1 / 1 - Math.Pow(Math.E, -sum));
        }
        //Вычисление Дифференциала от Сигмоида
        private double Differential_Sigmoid(double sig)
        {
            return sig * (1 - sig);
        }
        ///<summary>
        ///Прямое распространение сигналов
        ///</summary>
        public void FeedForward(List<double> input)
        {
            double dot = 0; //Сумма умножения входных сигналов и соотв. его веса; dot product of inputs & weights, i.e. (x1*w1)+(x2*w2)+...
            LoadWeights(input.Count);       //Иницилизация списка весов
            for (int i =0; i<input.Count;i++)   //Для каждого входного сигнала
            {
                dot += input[i]*weights[i];     //складываем текущую сумму и произведение входного сигнала и соотв его веса 
            }
            double net = dot + bais;            //складываем сумму и смещение персептрона
            FNet = Sigmoid(net);                //найдём f(net)
            DSigmoid = Differential_Sigmoid(FNet); //найдём Дифференциал f(net)
        }

        //Обратное распространение ошибок
        public void BackProp(double delta_weight,double delta_bais)
        {
            //foreach(double w in weights)
            //    w += delta_weight;
            for (int i = 0; i < weights.Count; i++) //Для каждого веса
                weights[i] += (delta_weight * learning_rate);   //корректируем вес
            bais += (delta_bais*learning_rate); //корректируем смещение
        }
        //Обратное распространение ошибок
        public void BackProp(double delta, List<double> inputs)
        {
            for (int i = 0; i < weights.Count; i++) //Для каждого веса
                weights[i] += (delta * inputs[i] * learning_rate);  //корректируем вес
            bais += delta * learning_rate;      //корректируем смещение
        }
    }
}