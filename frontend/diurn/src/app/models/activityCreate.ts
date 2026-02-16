export class ActivityCreate {
    name : string | null = null
    type: ActivityType | null = null
}

export class ActivityType {
    id: string | null = null
    name: string | null = null
    category: number | null = null
}

export class ActivityCard {
    id: string | null = null
    name: string | null = null
}

export class ActivityDetail {
    id: string | null = null
    name: string | null = null
    type: ActivityType | null = null
}