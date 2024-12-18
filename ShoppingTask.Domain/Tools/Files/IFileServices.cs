﻿namespace ShoppingTask.Domain.Tools.Files;

public interface IFileServices
{
    Task<string?> Upload(IFormFile? file);
    Task<List<string>> Upload(List<IFormFile>? imageFile);

    Task<string?> Modify(string? prop, IFormFile? file);
    Task<List<string>> Modify(List<string> prop, List<IFormFile> files, List<string> deleted);
    void Delete(string? path);
    void Delete(List<string>? path);
}
