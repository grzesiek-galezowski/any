using System;
using FluentAssertions;

namespace AnySpecification;

public class ReportedErrorsResolutionSpecification
{
  [Test]
  public void ShouldFillProperties()
  {
    //WHEN
    var uploadPayloadDto = Any.Instance<UploadPayloadDto>();

    //THEN
    uploadPayloadDto.MigrationData.Should().NotBeNull();
  }


  public class UploadPayloadDto
  {
    public MigrationDataDto? MigrationData { get; set; }
  }
  
  public record MigrationDataDto
  {
    public DateTime Time { get; set; }
  }
}

