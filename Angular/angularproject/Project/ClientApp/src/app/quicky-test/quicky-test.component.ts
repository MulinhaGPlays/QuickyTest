import { Component } from '@angular/core';
import { PromptModel } from '../sub-components/prompt-model/prompt-model.component';
import { HttpClient } from '@angular/common/http'

@Component({
  selector: 'app-quicky-test',
  templateUrl: './quicky-test.component.html',
  styleUrls: ['./quicky-test.component.css']
})
export class QuickyTestComponent {
  proofModel: ProofModel = new ProofModel();
  qtdConfirm: number = 0;
  qtdNotConfirm: number = 0;

  addProve() {
    let prompt = new PromptModel();
    this.proofModel.Prompts.push(prompt);
    this.verificarStatus();
  }
  verProvas(): void {
    console.log(this.proofModel.Prompts.filter(x => x.confirmado === true));
  }
  verificarStatus() {
    this.qtdConfirm = this.proofModel.Prompts.filter(x => x.confirmado === true).length;
    this.qtdNotConfirm = this.proofModel.Prompts.filter(x => x.confirmado === false).length;
  }

  receberAviso() {
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
