import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-visual-proof',
  templateUrl: './visual-proof.component.html',
  styleUrls: ['./visual-proof.component.css']
})
export class VisualProofComponent {
  @Input() gerando!: boolean;
  @Input() texto!: string;

  toggleAccordion(): void {
    this.gerando = !this.gerando;
  }
}
