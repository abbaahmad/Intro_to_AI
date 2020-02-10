using System;
using System.Collections.Generic;

namespace NeuralClassLibrary
{
    //����� �����������
    public class Perceptron
    {
        //������:
        List <double> weights;  //������ �����
        Random rnd;             //��������� ��������� �����
        double bais;            //��������
        double learning_rate;   //�������� ��������

        //��������:
        public double DSigmoid     //������ �.�. ������������ �� ��������
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
        public List<double> Weights //������ �����
        {
            get { return weights; }
        }
        public double Learning_rate //�������� ��������
        {
            set{learning_rate = value;}
            get{return learning_rate;}
        }

        //�������:
        public Perceptron()     //�����������
        {
            rnd = new Random();     //��������� ��������� �����
            weights = new List<double>();   //������ �����
            bais = rnd.NextDouble();        //��������
            learning_rate = 0.05;           //�������� ��������
            // (0,9)%10);
        }
        //������������ ����� 
        private void LoadWeights(int num)
        {
            if (weights.Count != 0) return;
            for (int i = 0; i < num; i++)
                weights.Add(rnd.NextDouble());
        }
        //���������� ��������
        private double Sigmoid(double sum)
        {
            return (1 / 1 - Math.Pow(Math.E, -sum));
        }
        //���������� ������������� �� ��������
        private double Differential_Sigmoid(double sig)
        {
            return sig * (1 - sig);
        }
        ///<summary>
        ///������ ��������������� ��������
        ///</summary>
        public void FeedForward(List<double> input)
        {
            double dot = 0; //����� ��������� ������� �������� � �����. ��� ����; dot product of inputs & weights, i.e. (x1*w1)+(x2*w2)+...
            LoadWeights(input.Count);       //������������ ������ �����
            for (int i =0; i<input.Count;i++)   //��� ������� �������� �������
            {
                dot += input[i]*weights[i];     //���������� ������� ����� � ������������ �������� ������� � ����� ��� ���� 
            }
            double net = dot + bais;            //���������� ����� � �������� �����������
            FNet = Sigmoid(net);                //����� f(net)
            DSigmoid = Differential_Sigmoid(FNet); //����� ������������ f(net)
        }

        //�������� ��������������� ������
        public void BackProp(double delta_weight,double delta_bais)
        {
            //foreach(double w in weights)
            //    w += delta_weight;
            for (int i = 0; i < weights.Count; i++) //��� ������� ����
                weights[i] += (delta_weight * learning_rate);   //������������ ���
            bais += (delta_bais*learning_rate); //������������ ��������
        }
        //�������� ��������������� ������
        public void BackProp(double delta, List<double> inputs)
        {
            for (int i = 0; i < weights.Count; i++) //��� ������� ����
                weights[i] += (delta * inputs[i] * learning_rate);  //������������ ���
            bais += delta * learning_rate;      //������������ ��������
        }
    }
}