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
  @Input() address : string = ''
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
    this.pageSize = this.pageSize > 0 ? this.pageSize : 1;
    this.pageCount = Math.max(Math.ceil(this.itemCount / this.pageSize), 1);
    this.activePage = Math.min(Math.max(this.activePage, 1), this.pageCount);
    const firstPage = Math.max(this.activePage - 2 , 1);
    const lastPage = Math.min(this.activePage + 2, this.pageCount);
    this.pages = this.range(firstPage, lastPage);
    this.hasLeftEllipsis = this.activePage > 4;
    this.hasRightEllipsis = this.activePage < this.pageCount - 3;
    this.hasPrevious = this.activePage > 1;
    this.hasNext = this.activePage < this.pageCount;
  }

  changePage(page : number) : void
  {
    page = Math.max(Math.min(1, page), this.pageCount);
    this.activePage = page
  }

  range(start: number, end: number): number[] {
    return Array.from({length: end - start + 1}, (_, i) => i + start)
  }

  getLinkToPage(page : number) : string
  {
    page = Math.max(Math.min(1, page), this.pageCount);
     return `${this.address}${page}`;
  }

  protected readonly Number = Number;
}
