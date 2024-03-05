import { Room } from "./room.Model";

export class RoomImage {
    imageId: number;
    roomId: number;
    imageUrl: string;
    room: Room | null;

    constructor(imageId: number, roomId: number, imageUrl: string, room: Room | null) {
        this.imageId = imageId;
        this.roomId = roomId;
        this.imageUrl = imageUrl;
        this.room = room;
    }
}
