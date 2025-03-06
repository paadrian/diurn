import { NgClass } from '@angular/common';
import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-pagination',
  imports: [NgClass],
  templateUrl: './pagination.component.html',
  styleUrl: './pagination.component.css'
})
export class PaginationComponent implements OnInit {
  @Input() activePage : number = 1
  @Input() pageSize : number = 0
  @Input() itemCount : number = 0
  pageCount : number = 0
  pages : number[] = []
  hasLeftEllipsis : boolean = false
  hasRightEllipsis : boolean = false
  hasPrevious : boolean = false
  hasNext : boolean = false

  /**
   *
   */
  ngOnInit(): void {
    this.pageCount = this.pageSize > 0 ? Math.ceil(this.itemCount / this.pageSize) : 1;
    this.activePage = this.pageSize > 0 ? this.activePage : 1;
    this.pages = this.range(1, this.pageCount);
    this.hasLeftEllipsis = this.activePage > 3;
    this.hasRightEllipsis = this.activePage < this.pageCount - 2;
  }

  changePage(page : number) : void
  {
    this.activePage = page
  }

  range(start: number, end: number): number[] {
    return Array.from({length: end - start + 1}, (_, i) => i + start)
  }
}
