import { Component, OnDestroy, OnInit, inject } from '@angular/core';

import { Subject } from 'rxjs';
import { UserService } from '../../services/user.service';

import { AsyncPipe } from '@angular/common';

@Component({
  selector: 'app-user',
  imports: [AsyncPipe],
  templateUrl: './user.component.html',
  styleUrl: './user.component.scss'
})
export class UserComponent implements OnInit, OnDestroy {
  userService = inject(UserService);
  private readonly _destroying$ = new Subject<void>();

  ngOnInit(): void {
    
  }

  ngOnDestroy(): void {
    this._destroying$.next();
    this._destroying$.complete();
  }
}
