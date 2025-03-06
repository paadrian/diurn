export class ActivityCreate {
    name : string | null = null
    type: ActivityTypeResponse | null = null
}

export class ActivityTypeResponse {
    id: string | null = null
    name: string | null = null
    category: number | null = null
}