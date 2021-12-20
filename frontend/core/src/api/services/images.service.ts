import api from "../api.helper";

const endpoint = "/api/core/posts/images";

interface IImage {
  id: string;
  url: string;
}

export class ImagesService {
  static async uploadImage(content: string): Promise<IImage> {
    return api.post(endpoint, { content });
  }
}
