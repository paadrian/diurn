import { Component, Input, OnInit } from "@angular/core";

@Component({
  selector: "app-root",
  templateUrl: "./page.component.html",
  styleUrl: "./page.component.css"
})
export class AppComponent implements OnInit{
    @Input() currentPage : number = 1
    @Input() activePage : number = 1
    @Input() pageCount : number = 0
    isActive : boolean = false
    isVisible : boolean = false

    ngOnInit(): void {
        this.isActive = this.currentPage === this.activePage;
        const isFirst = this.currentPage === 1 && this.activePage > 4;
        const isLast = this.currentPage === this.pageCount && this.activePage <= this.pageCount - 3;
        const isInRange = this.currentPage >= this.activePage - 2 && this.currentPage <= this.activePage + 2
        this.isVisible = this.isActive || isFirst || isLast || isInRange;
    }
}