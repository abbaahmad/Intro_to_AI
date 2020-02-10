using System;
using System.Collections.Generic;

namespace NeuralClassLibrary
{
    //����� ����
    public class Sloi
    {
        //������:
        List<Perceptron> perceplist;    //������ ������������
        //List<double> inputsignals;    
        List<double> outputsignals;     //������ �������� �������� 
        List<double> dSigmoidList;      //������ �������������� �� ��������
        List<double> delta;             //������ ������**

        //��������:
        public int Size //���-�� ������������
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
        public double Learning_rate //�������� ��������
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
        public List<double> Outputsignals   //������ �������� ��������
        {
            set{outputsignals = value;}
            get{return outputsignals;}
        }
        public List<List<double>> Weights   //������ ������� �����
        {
            get
            {
                List<List<double>> weights = new List<List<double>>();
                foreach (Perceptron p in perceplist)
                    weights.Add(p.Weights);
                return weights;
            }
        }
        public List<double> DSigmoidList    //������ �������������� �� ��������
        {
            set{dSigmoidList = value;}
            get{return dSigmoidList;}
        }

        //�������:
        public Sloi()   //�����������
        {
            perceplist = new List<Perceptron>(/*Size*/);    //������ ������������
            //inputsignals = new List<double>();            //
            outputsignals = new List<double>();             //������ �������� ��������
            dSigmoidList = new List<double>();              //������ �������������� �� ��������
            delta = new List<double>();                     //������
        }
        //������������ �������
        private void LoadLists()
        {
            if (perceplist.Count == 0)  //���� ������ ������
            {
                for (int i = 0; i < Size; i++)
                    perceplist.Add(new Perceptron());   //������������ ������
            }
            if(dSigmoidList.Count == 0) //���� ������ ������
            {
                for (int i = 0; i < Size; i++)
                    dSigmoidList.Add(new double());     //������������ ������
            }
            if(outputsignals.Count ==0)      //���� ������ ������
            {
                for(int i=0;i<perceplist.Count;i++)
                    outputsignals.Add(new double());    //������������ ������
            }
            if (delta.Count == 0)   //���� ������ ������
            {
                for (int i = 0; i < perceplist.Count; i++)
                    delta.Add(new double());    //������������ ������
            }
        }
        //������ ��������������� ��������
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
        // �������� ��������������� ������
        public void /*List<double>*/ BackProp(List<double> ErrorList, List<double> inputs)
        {
            //for (int i = 0; i < ErrorList.Count; i++)
            //{
            //    dSigmoidList[i] = perceplist[i].Delta;
            //}
            double delta_weight, delta_bais;
            for (int i = 0;i< perceplist.Count;i++) //��� ������� �����������
            {
                delta[i] = (ErrorList[i] * dSigmoidList[i]);    //���������� ������
                delta_weight = delta [i]* inputs[i];            //���������� ������ ����
                delta_bais = delta[i];                          //���������� ������ ��������
                perceplist[i].BackProp(delta_weight,delta_bais);       //�������� ��������������� ������
            }
            //return delta;
        }
        //�������� ��������������� ������
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