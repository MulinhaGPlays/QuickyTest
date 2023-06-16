import { Component, Input, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-prompt-model',
  templateUrl: './prompt-model.component.html',
  styleUrls: ['./prompt-model.component.css']
})

export class PromptModelComponent {
  confirmed: boolean = false;
  opened: boolean = false;

  @Input() prompt!: PromptModel;
  @Output() status: EventEmitter<boolean> = new EventEmitter();
  @Output() delete: EventEmitter<number> = new EventEmitter();

  Confirm(): void {
    this.confirmed = !this.confirmed;
    this.prompt.confirmado = this.confirmed;
    this.status.emit();
  }
  Delete(): void {
    this.delete.emit(this.prompt.id);
  }
  OpenModal(): void {
    this.opened = !this.opened;
  }
}

export class PromptModel {

  id: number;
  assunto: string;
  materia: string;
  serie: string;
  nivel: string;
  qtdquestoes: number;
  possuicontexto: boolean;
  confirmado: boolean;

  constructor(id: number)
  {
    this.id = id;
    this.assunto = "";
    this.materia = "";
    this.serie = "";
    this.nivel = "";
    this.qtdquestoes = 10;
    this.possuicontexto = false;
    this.confirmado = false;
  }
}
