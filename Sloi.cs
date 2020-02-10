using System;
using System.Collections.Generic;

namespace NeuralClassLibrary
{
    //Класс Слоя
    public class Sloi
    {
        //Данные:
        List<Perceptron> perceplist;    //Список персептронов
        //List<double> inputsignals;    
        List<double> outputsignals;     //Список выходных сигналов 
        List<double> dSigmoidList;      //Список дифференциалов от Сигмойда
        List<double> delta;             //Список дельта**

        //Свойства:
        public int Size //Кол-во персептронов
        {
            set;
            //{
            //    for (int i = 0; i < value; i++)
            //        perceplist.Add(new Perceptron());
        //}
        get;
        }
            //get { if( perceplist.Count == null)
            //        return; } }
        public double Learning_rate //Скорость обучения
        {
            get { return perceplist[0].Learning_rate; }
            set
            {
                foreach (Perceptron p in perceplist)
                    p.Learning_rate = value;
            }
        }
        public List<double> Deltas  //**
        {
            set { delta = value; }
            get { return delta; }
        }
        public List<double> Outputsignals   //Список выходных сигналов
        {
            set{outputsignals = value;}
            get{return outputsignals;}
        }
        public List<List<double>> Weights   //Список списков весов
        {
            get
            {
                List<List<double>> weights = new List<List<double>>();
                foreach (Perceptron p in perceplist)
                    weights.Add(p.Weights);
                return weights;
            }
        }
        public List<double> DSigmoidList    //Список дифференциалов от Сигмойда
        {
            set{dSigmoidList = value;}
            get{return dSigmoidList;}
        }

        //Функции:
        public Sloi()   //Конструктор
        {
            perceplist = new List<Perceptron>(/*Size*/);    //список персептронов
            //inputsignals = new List<double>();            //
            outputsignals = new List<double>();             //список выходных сигналов
            dSigmoidList = new List<double>();              //список дифференциалов от Сигмойда
            delta = new List<double>();                     //ошибка
        }
        //Иницилизация списков
        private void LoadLists()
        {
            if (perceplist.Count == 0)  //если список пустой
            {
                for (int i = 0; i < Size; i++)
                    perceplist.Add(new Perceptron());   //иницилизация списка
            }
            if(dSigmoidList.Count == 0) //если список пустой
            {
                for (int i = 0; i < Size; i++)
                    dSigmoidList.Add(new double());     //иницилизация списка
            }
            if(outputsignals.Count ==0)      //если список пустой
            {
                for(int i=0;i<perceplist.Count;i++)
                    outputsignals.Add(new double());    //иницилизация списка
            }
            if (delta.Count == 0)   //если список пустой
            {
                for (int i = 0; i < perceplist.Count; i++)
                    delta.Add(new double());    //иницилизация списка
            }
        }
        //Прямое распространение сигналов
        public /*List<double>*/void Feedforward(List<double> inputs)
        {
            LoadLists();
            for (int i = 0;i<perceplist.Count; i++)
            {
                perceplist[i].FeedForward(inputs);
                outputsignals[i] = perceplist[i].FNet;
            }
            for (int i = 0; i < perceplist.Count; i++)
            {
                dSigmoidList[i] = perceplist[i].DSigmoid;
            }
        }
        /// <summary>
        /// Backpropagation of errors
        /// </summary>
        /// <param name="ErrorList">Cummilative error up to this layer starting from the end</param>
        /// <param name="inputs">This is the list of activations of the previous layer</param>
        // Обратное распространение ошибок
        public void /*List<double>*/ BackProp(List<double> ErrorList, List<double> inputs)
        {
            //for (int i = 0; i < ErrorList.Count; i++)
            //{
            //    dSigmoidList[i] = perceplist[i].Delta;
            //}
            double delta_weight, delta_bais;
            for (int i = 0;i< perceplist.Count;i++) //Для каждого персептрона
            {
                delta[i] = (ErrorList[i] * dSigmoidList[i]);    //вычисление ошибки
                delta_weight = delta [i]* inputs[i];            //вычисление дельту веса
                delta_bais = delta[i];                          //вычисление дельту смещения
                perceplist[i].BackProp(delta_weight,delta_bais);       //Обратное распространение дельта
            }
            //return delta;
        }
        //Обратное распространение ошибок
        public void BackProp(List<double> ErrorList, List<double> inputs,List<List<double>> nextweights)
        {
            for (int i = 0; i < perceplist.Count; i++)
                dSigmoidList[i] = perceplist[i].DSigmoid;

            for (int i = 0; i < /*perceplist*/nextweights.Count;i++)
            {
                double delta_value = 0;
                for (int j = 0; j< /*perceplist.Countnextweights*/ErrorList.Count; j++)
                {
                    delta_value += nextweights/*[i][j]*/[j][i] * ErrorList[j];
                }
                delta_value = delta_value * dSigmoidList[i];
                delta.Add(delta_value);
                perceplist[i].BackProp(delta[i], inputs);
            }
            /*for (int i = 0; i < ErrorList.Count; i++)
                dSigmoidList[i] = perceplist[i].Delta;
            //double delta_weight, delta_bais;
            double delta_value=0;
            for (int i = 0; i < perceplist.Count; i++)
            {
                List<double> listOfW = GetWeights(nextweights, i); 
                for (int j = 0; j < listOfW.Count/*nextweights[i].Count*//*; j++)
                {
                    delta_value =+ ErrorList[i] * dSigmoidList[i] * listOfW[j];//nextweights[j][i];
                }
                //delta.Add(ErrorList[i] * dSigmoidList[i] * nextweights[i][j]); //needs work
                delta.Add(delta_value);
                //delta_weight = delta[i] * inputs[i];
                //delta_bais = delta[i];
                //perceplist[i].BackProp(delta_weight, delta_bais);
                perceplist[i].BackProp(delta[i], inputs);
            }*/
        }
        private List<double> GetWeights(List<List<double>> ListOfWeights, int index)
        {
            List<double> W = new List<double>();
            foreach(List<double> l in ListOfWeights)
            {
                W.Add(l[index]);
            }
            return W; 
        }
    }
}