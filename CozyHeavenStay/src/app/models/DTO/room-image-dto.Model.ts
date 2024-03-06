export class RoomImageDTO {
    roomId: number;
    imageUrl: string;

    constructor(roomId: number, imageUrl: string) {
        this.roomId = roomId;
        this.imageUrl = imageUrl;
    }
}