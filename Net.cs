using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralClassLibrary
{
    //Класс Сети
    public class Net
    {
        //Данные:
        List<Sloi> sloii;   //Список слоёв

        //Функции:
        public Net()    //Конструктор
        {
            sloii = new List<Sloi>();   //Создание списка слоев
        }
        public Net(string[] networkStruct)  //конструктор
        {
            sloii = new List<Sloi>(networkStruct.Length);   //Создание списка с определеным размером
            for (int i = 0; i < networkStruct.Length; i++)  
            {
                sloii.Add(new Sloi());      //
                sloii[i].Size = int.Parse(networkStruct[i]);    //
            }
        }
        //Обучение
        public void Learn(List<double> inputlist, List<double> targets)
        {
            int i = 0;
            List<double> inputs = inputlist;        //Список вводных сигналов
            List<double> results = new List<double>();  //Список выходных сигналов сети
            List<double> errors = new List<double>();   //Список ошибок
            List<double> delta = new List<double>();    //Список дельта
            List<double> nextlayerWeights = new List<double>(); //Список весов следующего слоя
            List<double> sigmoidList = new List<double>();      //Список дифференциалов 

            for (i = 0;i<sloii.Count; i++)      //Для каждого слоя            
            {
                sloii[i].Feedforward(inputs);   //прямое распрастранение сигналов
                inputs = sloii[i].Outputsignals;    //сохранение выходных сигналов текущего слоя
            }
            results = sloii[sloii.Count - 1].Outputsignals; //угадание 
            sigmoidList = sloii[sloii.Count - 1].DSigmoidList;
            errors = new List<double>();
            for (i = 0; i < results.Count; i++)
            {
                errors.Add(results[i] - targets[i]);
                //delta.Add(errors[i] * sigmoidList[i] * inputlist[i]);
            }
            sloii[sloii.Count - 1].BackProp(errors,sloii[sloii.Count - 2].Outputsignals);
            for(i = sloii.Count -2; i> 0; i--)
            {
                sloii[i].BackProp(sloii[i + 1].Deltas, sloii[i - 1].Outputsignals, sloii[i + 1].Weights);
            }
            sloii[0].BackProp(sloii[1].Deltas, inputlist, sloii[1].Weights);
        }
        public List<double> Test(List<double> inputs)
        {
            //sloiList[0].Feedforward(inputs);
            List<double> nextLayer_inputs = inputs;
            for (int i = 0; i < sloii.Count; i++)
            {
                sloii[i].Feedforward(nextLayer_inputs);
                nextLayer_inputs = sloii[i].Outputsignals;
            }
            return nextLayer_inputs;
        }
    }
}
