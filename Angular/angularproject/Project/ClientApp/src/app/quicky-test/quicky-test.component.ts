import { Component, Inject } from '@angular/core';
import { PromptModel } from '../sub-components/prompt-model/prompt-model.component';
import { HttpClient } from '@angular/common/http'

@Component({
  selector: 'app-quicky-test',
  templateUrl: './quicky-test.component.html',
  styleUrls: ['./quicky-test.component.css']
})
export class QuickyTestComponent {
  proofModel: ProofModel = new ProofModel();
  http: HttpClient;
  baseUrl: string;

  qtdConfirm: number = 0;
  qtdNotConfirm: number = 0;
  showOffCanvas: boolean = false;
  generating: boolean = false;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.http = http;
    this.baseUrl = baseUrl;
    this.http.get(this.baseUrl + 'teste').subscribe(x => this.receberRequisicao(x))
  }

  receberRequisicao(value: any): void {
    console.log(value);
  }

  toggleOffCanvas(): void {
    this.showOffCanvas = !this.showOffCanvas;
  }
  addProve(): void  {
    let prompt = new PromptModel(this.proofModel.Prompts.length);
    this.proofModel.Prompts.push(prompt);
    this.verificarStatus();
  }
  verProvas(): void {
    console.log(this.proofModel);
    this.generating = true;
  }
  verificarStatus() {
    this.qtdConfirm = this.proofModel.Prompts.filter(x => x.confirmado === true).length;
    this.qtdNotConfirm = this.proofModel.Prompts.filter(x => x.confirmado === false).length;
  }

  receberAviso() {
    this.verificarStatus();
  }
  deleteProof(id: number) {
    let index: number = this.proofModel.Prompts.findIndex(prompt => prompt.id === id);
    this.proofModel.Prompts.splice(index, 1);
    this.verificarStatus();
  }
}

export class ProofModel {
  API_KEY: string;
  VisualReturn: boolean;
  Prompts: PromptModel[];

  constructor() {
    this.API_KEY = "";
    this.VisualReturn = true;
    this.Prompts = [];
  }
}

export class ResponseProof {
  pdfURL: string;
  wordURL: string;
  content: string;
  status: string;
  uuid_usuario: string;
  uuid_prova: string;

  constructor() {
    this.pdfURL = "";
    this.wordURL = "";
    this.content = "";
    this.status = "";
    this.uuid_usuario = "";
    this.uuid_prova = "";
  }
}
