import { Component, Inject } from '@angular/core';
import { PromptModel } from '../sub-components/prompt-model/prompt-model.component';
import { HttpClient, HttpResponse } from '@angular/common/http'
import { environment } from '../../environments/environment';
import { map, Observable, tap } from 'rxjs';

@Component({
  selector: 'app-quicky-test',
  templateUrl: './quicky-test.component.html',
  styleUrls: ['./quicky-test.component.css']
})
export class QuickyTestComponent {
  model: ProofModel = new ProofModel();
  private apiUrl = environment.apiUrl;
  http: HttpClient;
  respostaProva: string = "";

  qtdConfirm: number = 0;
  qtdNotConfirm: number = 0;
  showOffCanvas: boolean = false;
  generating: boolean = false;

  constructor(http: HttpClient) {
    this.http = http;
  }

  receberRequisicao(value: any): void {
    console.log(value);
  }
  tratamentoDeProva(prova: any) {
    console.log(prova)
    this.respostaProva += prova.content;
  }

  toggleOffCanvas(): void {
    this.showOffCanvas = !this.showOffCanvas;
  }
  addProve(): void  {
    let prompt = new PromptModel(this.model.Prompts.length);
    this.model.Prompts.push(prompt);
    this.verificarStatus();
  }
  verProvas(): void {
    this.generating = true;
    let a = this.respostaEmTempoReal();
    a.subscribe(x => console.log(x.body))
  }


  respostaEmTempoReal(): Observable<HttpResponse<any>> {
    return this.http.post<any>(this.apiUrl + 'proof/Generating', this.model, {
      observe: 'response',
      responseType: 'json',
      reportProgress: true,
    })
  }

  verificarStatus() {
    this.qtdConfirm = this.model.Prompts.filter(x => x.confirmado === true).length;
    this.qtdNotConfirm = this.model.Prompts.filter(x => x.confirmado === false).length;
  }

  receberAviso() {
    this.verificarStatus();
  }
  deleteProof(id: number) {
    let index: number = this.model.Prompts.findIndex(prompt => prompt.id === id);
    this.model.Prompts.splice(index, 1);
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
