import { Component, inject, OnInit } from '@angular/core';
import { PaginationComponent } from "../pagination/pagination.component";
import { ActivityService } from '../services/activity.service';
import { PageFilter } from '../models/pageFilter';
import { ActivityDetail } from '../models/activityCreate';
import { UserProfile } from '../models/userProfile';

@Component({
  selector: 'app-activity-list',
  imports: [PaginationComponent],
  templateUrl: './activity-list.component.html',
  styleUrl: './activity-list.component.scss'
})
export class ActivityListComponent implements OnInit {
  activePage : number = 1;
  pageSize: number = 10;
  activities : ActivityDetail[] = [];
  userprofile : UserProfile = new UserProfile();
  private readonly activityService = inject(ActivityService);
  
  ngOnInit(): void
  {
    this.refreshActivities();
  }

  refreshActivities() : void
  {
    const pageFilter : PageFilter = { pageNo: this.activePage, pageSize: this.pageSize };
    this.activityService.getActivities(pageFilter).subscribe({
      next: (data) => {
        this.activities = data;
      },
      error: (err) => {
        console.error(err);
      }
    });
  }

  changePage(page: number) : void
  {
    this.activePage = page;
  }
}
