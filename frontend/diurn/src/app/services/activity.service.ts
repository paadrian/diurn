import { Injectable } from "@angular/core";
import { PageFilter } from "../models/pageFilter";
import { ActivityDetail } from "../models/activityCreate";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";

@Injectable({
    providedIn: 'root'})
export class ActivityService {
    url = 'https://localhost:5001/api/activities';

    constructor(private readonly http: HttpClient) {}

    getActivities(pageFilter : PageFilter) : Observable<ActivityDetail[]> {
        return this.http.get<ActivityDetail[]>(`${this.url}`);
        // return this.http.get<ActivityDetail[]>(`${this.url}?pageNo=${pageFilter.pageNo}&pageSize=${pageFilter.pageSize}`);
    }

    getActivity(id: string) : Observable<ActivityDetail> {
        return this.http.get<ActivityDetail>(`${this.url}/${id}`);
    }
}
