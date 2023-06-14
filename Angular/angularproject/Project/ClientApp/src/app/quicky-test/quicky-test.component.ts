import { Component } from '@angular/core';
import { PromptModel } from '../sub-components/prompt-model/prompt-model.component';

@Component({
  selector: 'app-quicky-test',
  templateUrl: './quicky-test.component.html',
  styleUrls: ['./quicky-test.component.css']
})
export class QuickyTestComponent {
  prompts: PromptModel[] = []

  addProve() {
    let prompt = new PromptModel();
    this.prompts.push(prompt);
  }
}
