import { Component, Input, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-prompt-model',
  templateUrl: './prompt-model.component.html',
  styleUrls: ['./prompt-model.component.css']
})

export class PromptModelComponent {
  confirmed: boolean = false;

  @Input() prompt!: PromptModel;
  @Output() status: EventEmitter<boolean> = new EventEmitter();

  Confirm() {
    this.confirmed = !this.confirmed;
    this.prompt.confirmado = this.confirmed;
    this.status.emit();
  }
}

export class PromptModel {

  assunto: string;
  materia: string;
  serie: string;
  nivel: string;
  qtdquestoes: number;
  possuicontexto: boolean;
  confirmado: boolean;

  constructor()
  {
    this.assunto = "";
    this.materia = "";
    this.serie = "";
    this.nivel = "";
    this.qtdquestoes = 10;
    this.possuicontexto = false;
    this.confirmado = false;
  }
}
