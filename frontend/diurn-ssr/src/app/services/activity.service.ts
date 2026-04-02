import { Injectable } from "@angular/core";

@Injectable({
    providedIn: 'root'})
export class ActivityService {
    url = 'https://localhost:5001/api/activities';

    async getActivities() {
        const response = await fetch(this.url);
        const data = await response.json() ?? [];
        return data;
    }

    async getActivity(id: string) {
        const response = await fetch(`${this.url}/${id}`);
        const data = await response.json();
        return data;
    }
}