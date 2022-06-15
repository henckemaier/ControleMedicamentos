using ControleMedicamentos.Dominio.ModuloMedicamento;
using ControleMedicamentos.Dominio.ModuloPaciente;
using System;
using System.Collections.Generic;

namespace ControleMedicamentos.Dominio.ModuloRequisicao
{
    public class Requisicao : EntidadeBase<Requisicao>
    {   
        public Requisicao()
        {
        }

        public Medicamento Medicamento { get; set; }
        public Paciente Paciente { get; set; }
        public int QtdMedicamento { get; set; }
        public DateTime Data { get; set; }
        public ModuloFuncionario.Funcionario Funcionario { get; set; }

        public Requisicao(int qtdMedicamento, DateTime data)
        {
            QtdMedicamento = qtdMedicamento;
            Data = data;
        }

        public override void Atualizar(Requisicao registro)
        {
            QtdMedicamento = registro.QtdMedicamento;
            Data = registro.Data;
        }
    }
}
