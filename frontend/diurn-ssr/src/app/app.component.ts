import { Component } from '@angular/core';
import { RouterLink, RouterLinkActive, RouterOutlet} from '@angular/router';
import { PaginationComponent } from './pagination/pagination.component';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, PaginationComponent, RouterLink, RouterLinkActive],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'diurn';
  currentPage : number = 1

  changePage(page : number) : void
  {
    this.currentPage = page
  }
}
