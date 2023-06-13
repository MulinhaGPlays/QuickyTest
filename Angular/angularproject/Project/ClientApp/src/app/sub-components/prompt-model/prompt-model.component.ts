import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-prompt-model',
  templateUrl: './prompt-model.component.html',
  styleUrls: ['./prompt-model.component.css']
})

export class PromptModelComponent {
  @Input() prompt!: PromptModel;

  changeAssunto(event: any): void {
    this.prompt.assunto = event.target.value;
  }
  changeMateria(event: any): void {
    this.prompt.materia = event.target.value;
  }
  changeSerie(event: any): void {
    this.prompt.serie = event.target.value;
  }
  changeNivel(event: any): void {
    this.prompt.nivel = event.target.value;
  }
  changeQtdQuestoes(event: any): void {
    this.prompt.qtdquestoes = event.target.value;
  }
  changePossuiContexto(event: any): void {
    this.prompt.possuicontexto = event.checked;
  }
}

export class PromptModel {

  assunto: string;
  materia: string;
  serie: string;
  nivel: string;
  qtdquestoes: number;
  possuicontexto: boolean;

  constructor()
  {
    this.assunto = "";
    this.materia = "";
    this.serie = "";
    this.nivel = "";
    this.qtdquestoes = 10;
    this.possuicontexto = false;
  }
}
